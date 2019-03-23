using MyPOS2.Dal;
using MyPOS2.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class TicketBL
    {
        internal static TrTicketViewModel FillTicket(string numTransaction)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                //find transac
                var transac = TransactionBL.FindTransactionById(numTransaction);
                //create ticket
                TrTicketViewModel vm = new TrTicketViewModel();
                //vm.Ticket = (dal.CreateTicket()).ToString();

                vm.DateTicket = (DateTime.Now).ToString();
                //n° transac
                vm.Transaction = numTransaction;
                //to do --> magasin

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
                //to do --> provisoire language = 1 = French
                int language = 1;
                var messages = FindTicketMessageById(transac.idTransaction, language);
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
                List<int?> messageIds = dal.GetListMessagesId(transacId);
                if (messageIds.Count == 0 || messageIds == null)
                {
                    //message par défaut
                    messageIds.Add(1);
                    dal.UpdateTransactionMessage(transacId, 1);
                }
                return dal.GetListTicketMessageByIdAndLanguage(messageIds, languageMessage);
            }
        }
    }
}