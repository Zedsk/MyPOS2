using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using MyPOS2.Models.Transactions.Ticket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class TicketBL
    {
        internal static TrTicketViewModel FillTicket(string numTransaction, string language)
            {
            using (IDalTransaction dal = new DalTransaction())
            {
                //find transac
                var transac = TransactionBL.FindTransactionById(numTransaction);
                //create ticket
                TrTicketViewModel vm = new TrTicketViewModel();
                //to do --> provisoire language = 1 = French
                int lang;
                if (int.TryParse(language, out int codeL))
                {
                    lang = codeL;
                }
                else
                {
                    lang = LanguageBL.FindIdLanguageByShortForm(language);
                }
                vm.Language = lang.ToString();

                //if rprint search dateTicket
                DateTime d = new DateTime(2000, 1, 1, 0, 0, 0);
                int dCompare = DateTime.Compare(transac.transactionDateEnd, d);
                if (dCompare == 0)
                {
                    vm.DateTicket = (DateTime.Now).ToString();
                }
                else
                {
                    vm.DateTicket = transac.transactionDateEnd.ToString();
                }
                //n° transac
                vm.Transaction = numTransaction;
                //to do --> magasin
                vm.Shop = ShopBL.FindShopById(transac.shopId, lang);
                //detail
                vm.DetailsListWithTot = TransactionBL.ListDetailsWithTot(numTransaction);

                //discount

                if (transac.discountGlobal == null)
                {
                    vm.DiscountG = " - ";
                }
                else
                {
                    vm.DiscountG = (transac.discountGlobal).ToString();
                }

                ////VAT
                //vm.VatG = (FindVatValById(transac.vatId)).ToString();
                //vm.VatG = dal.GetAppliedVatById(transac.vatId).appliedVat;

                //Total
                vm.TotalG = (transac.total).ToString();

                //payment method & amount
                vm.Payments = PaymentBL.FindPaymentsByTransacId(numTransaction);

                ////message
                //var message = FindTicketMessageById(transac.messageId, transac.languageId);
                
                var messages = FindTicketMessageById(transac.idTransaction, lang);
                vm.Messages = messages;
                return vm;
            }
        }

        //private static string FindTicketMessageById(int messageId, int languageMessage)
        //{
        //    using (IDalTicket dal = new DalTicket())
        //    {
        //        return dal.GetTicketMessagesByIdsAndLanguage(messageId, languageMessage);
        //    }
        //}

        private static List<string> FindTicketMessageById(int transacId, int languageMessage)
        {
            using (IDalTicket dal = new DalTicket())
            {
                List<int?> messageIds = dal.GetListTransactionMessageId(transacId);
                if (messageIds.Count == 0 || messageIds == null)
                {
                    //message par défaut
                    int messageId = 1;
                    messageIds.Add(messageId);
                    dal.CreateTransactionMessage(transacId, messageId, languageMessage);
                }
                return dal.GetListTicketMessageTransByIdAndLanguage(messageIds, languageMessage);
            }
        }

        internal static IList<TrTicketViewModel> FindTicket(TrRprintTicketViewModel vmodel)
        {

            using (IDalTicket dal = new DalTicket())
            {
                IList<int> listTransac = new List<int>();
                IList<int> listFilter = new List<int>();
                IList<TrTicketViewModel> result = new List<TrTicketViewModel>();
                DateTime dateMin;
                DateTime dateMax;
                TimeSpan timeDay = vmodel.TimeDay.TimeOfDay;
                DateTime dateDay = vmodel.DateDay.Add(timeDay);
                if (vmodel.TimeSure)
                {
                    listTransac = dal.GetTicket(dateDay);
                }
                // si listTransac.Count() = 0 --> erreur estimation du client
                if (listTransac.Count() == 0 || listTransac == null)
                {
                    //to do -> rendre les param (-+ 30) dynamiques
                    dateMin = dateDay.AddMinutes(-30);
                    dateMax = dateDay.AddMinutes(30);
                    listTransac = dal.GetTicket(dateMin, dateMax);
                    if (listTransac.Count() > 1)
                    {
                        if (vmodel.GlobalTotal != null)
                        {
                            decimal total = decimal.Parse(vmodel.GlobalTotal);
                            listTransac = dal.GetTicket(dateMin, dateMax, total);
                        }
                        if (listTransac.Count() > 1)
                        {
                            if (vmodel.Language != null)
                            {
                                int idLanguage = int.Parse(vmodel.Language);
                                listFilter = dal.GetTicket(idLanguage);
                                listTransac = listTransac.Where(i => i.Equals(listFilter)).ToList();
                            }
                        }
                        if (listTransac.Count() > 1)
                        {
                            if (vmodel.MethodP != null)
                            {
                                //to do
                                int idMethodP = int.Parse(vmodel.MethodP);
                            }
                        }
                        if (listTransac.Count() > 1)
                        {
                            if (vmodel.NbItem != null)
                            {
                                //to do
                                int nbItem = int.Parse(vmodel.NbItem);
                            }
                        }
                    }
                }
                for (int i = 0; i < listTransac.Count(); i++)
                {
                    if (vmodel.Language == null)
                    {
                        vmodel.Language = FindLanguageTicketByIdTransac(listTransac[i].ToString());
                    }
                    result.Add(FillTicket(listTransac[i].ToString(), vmodel.Language));
                }
                return result;
            }
        }

        private static string FindLanguageTicketByIdTransac(string id)
        {
            using (IDalTicket dal = new DalTicket())
            {
                int idTransac = int.Parse(id);
                int result = dal.GetLanguageTicketByIdTransac(idTransac);
                return result.ToString();
            }
        }
    }
}