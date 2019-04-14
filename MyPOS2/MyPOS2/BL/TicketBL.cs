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
        internal static TrTicketViewModel FillTicket(string numTransaction, string language, bool? isChange)
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
                    var temp = (transac.discountGlobal * 100).ToString();
                    var tempsplit = temp.Split(',');
                    var discount = tempsplit[0] + "%";
                    vm.DiscountG = discount;
                }

                ////VAT
                //vm.VatG = (FindVatValById(transac.vatId)).ToString();
                //vm.VatG = dal.GetAppliedVatById(transac.vatId).appliedVat;
                //to do --> provisoire vatId = 2 --> 21%
                int tva = 2;
                vm.VatG = VatBL.FindVatValById(tva);


                //Total
                vm.TotalG = (transac.total).ToString();

                //payment method & amount
                vm.Payments = PaymentBL.FindPaymentsByTransacId(numTransaction);
                //foreach (var item in vm.Payments)
                //{
                //    //var test = item.PAYMENT_METHOD.method;
                //    string test2 = PaymentBL.FindMethodNameById(item.paymentMethodId);
                //    item.PAYMENT_METHOD.method = test2;

                //}

                ////message
                //var message = FindTicketMessageById(transac.messageId, transac.languageId);

                var messages = FindTicketMessageById(transac.idTransaction, lang, isChange);
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

        //private static List<string> FindTicketMessageById(int transacId, int languageMessage)
        //{
        //    using (IDalTicket dal = new DalTicket())
        //    {
        //        List<int?> messageIds = dal.GetListIdTransactionMessage(transacId);
        //        if (messageIds.Count == 0 || messageIds == null)
        //        {
        //            message par défaut
        //            int messageId = 1;
        //            messageIds.Add(messageId);
        //            dal.CreateTransactionMessage(transacId, messageId, languageMessage);
        //        }
        //        return dal.GetListTicketMessageTransByIdAndLanguage(messageIds, languageMessage);
        //    }
        //}

        private static List<string> FindTicketMessageById(int transacId, int languageMessage, bool? isChange)
        {
            using (IDalTicket dal = new DalTicket())
            {
                List<int?> messageIds = dal.GetListIdTransactionMessage(transacId);
                
                if (messageIds.Count == 0 || messageIds == null)
                {
                    //message par défaut
                    int messageId = 1;
                    messageIds.Add(messageId);
                    dal.CreateTransactionMessage(transacId, messageId, languageMessage);
                }
                else
                {
                    //verify and update if language message is changed
                    if (isChange != null && isChange == true)
                    {
                        dal.UpdateTransactionMessageLanguage(transacId, languageMessage);
                    }
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
                        if (vmodel.Client != null)
                        {
                            int client = int.Parse(vmodel.Client);
                            listTransac = dal.GetTicket(dateMin, dateMax, client);
                        }
                        if (listTransac.Count() > 1)
                        {
                            if (vmodel.GlobalTotal != null)
                            {
                                decimal total = decimal.Parse(vmodel.GlobalTotal);
                                listTransac = dal.GetTicket(dateMin, dateMax, total);
                            }
                        }
                        if (listTransac.Count() > 1)
                        {
                            if (vmodel.Language != null)
                            {
                                int idLanguage = int.Parse(vmodel.Language);
                                listFilter = dal.GetTicket(idLanguage);
                                var listTemp = new List<int>();
                                for (int i = 0; i < listTransac.Count; i++)
                                {
                                    if (listFilter.Contains(listTransac[i]))
                                    {
                                        listTemp.Add(listTransac[i]);
                                    }
                                }
                                listTransac = listTemp;
                            }
                        }
                        if (listTransac.Count() > 1)
                        {
                            if (vmodel.NbItem != null)
                            {
                                //to do
                                int nbItem = int.Parse(vmodel.NbItem);
                                listFilter = dal.GetTicketNbItem(dateMin, dateMax, nbItem);
                                var listTemp = new List<int>();
                                for (int i = 0; i < listTransac.Count; i++)
                                {
                                    if (listFilter.Contains(listTransac[i]))
                                    {
                                        listTemp.Add(listTransac[i]);
                                    }
                                }
                                listTransac = listTemp;
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
                    }
                    else if (listTransac.Count() == 0)
                    {
                        listTransac = dal.GetAllTicketDay(dateDay);
                    }
                }
                for (int i = 0; i < listTransac.Count(); i++)
                {
                    string lang;
                    if (vmodel.Language == null)
                    {
                        lang = FindLanguageTicketByIdTransac(listTransac[i].ToString());
                    }
                    else
                    {
                        //verify if if vmodel.Language = ticket language
                        string ticketL = FindLanguageTicketByIdTransac(listTransac[i].ToString());
                        if (vmodel.Language != ticketL)
                        {
                            continue;
                        }
                        else
                        {
                            lang = vmodel.Language;
                        }
                    }
                    //to do --> change init isChange...
                    bool isChange = false;
                    result.Add(FillTicket(listTransac[i].ToString(), lang, isChange));
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