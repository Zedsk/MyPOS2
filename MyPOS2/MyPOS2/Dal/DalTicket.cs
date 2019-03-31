using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;
using MyPOS2.Models.Transactions.Ticket;

namespace MyPOS2.Dal
{
    public class DalTicket : IDalTicket
    {
        #region DB

        private Pos1Entities db;

        public DalTicket()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        //public int CreateTicket()
        //{
        //    // to do --> provisoire messageId = 1 , languageId = 1
        //    TICKET t = new TICKET { messageId = 1, languageId = 1 };
        //    db.TICKETs.Add(t);
        //    db.SaveChanges();
        //    return t.idTicket;
        //}

        //public string GetTicketMessagesByIdsAndLanguage(int messageId, int languageMessage)
        //{
        //    var ticket = db.TICKET_MESSAGEs.Where(t => t.idMessages == messageId && t.languageId == languageMessage).Single();
        //    return ticket.message;
        //}

        public string GetTicketMessageTransByIdAndLanguage(int idMessage, int languageMessage)
        {
            var ticket = db.MESSAGE_TRANSLATIONs.Where(t => t.messageId == idMessage && t.languageId == languageMessage).Single();
            return ticket.message;
        }

        public List<string> GetListTicketMessageTransByIdAndLanguage(List<int?> idMessages, int languageMessage)
        {
            List<string> result = new List<string>();
            List<MESSAGE_TRANSLATION> listMessages = db.MESSAGE_TRANSLATIONs.Where(m => m.languageId == languageMessage).ToList();
            for (int i = 0; i < idMessages.Count(); i++)
            {
                result.Add(listMessages.Find(m => m.messageId == idMessages[i] && m.languageId == languageMessage).message);
            }
            return result;
        }

        public List<int?> GetListIdTransactionMessage(int transacId)
        {
            List<int?> messageIds = new List<int?>();
            messageIds = db.SPP_TransactionMessageIds(transacId).ToList();
            return messageIds;
        }

        public List<TRANSACTIONS_MESSAGE> GetListTransactionMessage(int transacId)
        {
            return db.TRANSACTIONS_MESSAGEs.Where(m => m.transactionId == transacId).ToList();
        }

        public void UpdateTransactionMessageLanguage(int transacId, int languageMessage)
        {
            List<TRANSACTIONS_MESSAGE> messages = GetListTransactionMessage(transacId);
            foreach (var item in messages)
            {
                if (item.languageMessage != languageMessage)
                {
                    item.languageMessage = languageMessage;
                }
            }
            db.SaveChanges();
        }

            

    //// lorsque TRANSACTIONS_MESSAGE ne contenait pas languageMessage
    //public void CreateTransactionMessage(int transacId, int messageId)
    //{
    //    TICKET_MESSAGE tMessage = db.TICKET_MESSAGEs.FirstOrDefault(m => m.idMessage == messageId);
    //    db.TRANSACTIONSs.FirstOrDefault(t => t.idTransaction == transacId).TICKET_MESSAGE.Add(tMessage);
    //    db.SaveChanges();
    //}

    public void CreateTransactionMessage(int transacId, int messagId, int langMessage)
        {
            TRANSACTIONS_MESSAGE tMessage = new TRANSACTIONS_MESSAGE { transactionId = transacId, messageId = messagId, languageMessage = langMessage };
            db.TRANSACTIONS_MESSAGEs.Add(tMessage);
            db.SaveChanges();
        }



        //public IList<int> GetTicket(DateTime date)
        //{
        //    IList<int> result = new List<int>();
        //    // statusId = 2  = transaction end   +  isReturn = false  = is not a Transtacion return
        //    //Normaly Count(result) = 1
        //    IList<TRANSACTIONS> transacs = db.TRANSACTIONSs.Where(d => d.transactionDateEnd.Date == date.Date 
        //                                                            && d.transactionDateEnd.Hour == date.Hour
        //                                                            && d.transactionDateEnd.Minute == date.Minute
        //                                                            && d.statusId == 2 && d.isReturn == false).ToList();
        //    foreach (var item in transacs)
        //    {
        //        result.Add(item.idTransaction);
        //    }
        //    return result;
        //}

        public IList<int> GetTicket(DateTime date)
        {
            IList<int?> res = new List<int?>();
            IList<int> result = new List<int>();
            // statusId = 2  = transaction end   +  isReturn = false  = is not a Transtacion return
            //Normaly Count(result) = 1
            res = db.SPP_TicketTimeSure(date.Year, date.Month, date.Day, date.Hour, date.Minute, 2, false).ToList();
            for (int i = 0; i < res.Count(); i++)
            {
                result.Add(res[i].Value);
            }
            return result;
        }

        public IList<int> GetTicket(DateTime dateMin, DateTime dateMax)
        {
            IList<int> result = new List<int>();
            // statusId = 2  = transaction end   +  isReturn = false  = is not a Transtacion return
            IList<TRANSACTIONS> transacs = db.TRANSACTIONSs.Where(d => d.transactionDateEnd >= dateMin && d.transactionDateEnd <= dateMax && d.statusId == 2  && d.isReturn == false).ToList();
            foreach (var item in transacs)
            {
                result.Add(item.idTransaction);
            }
            return result;
        }

        public IList<int> GetTicket(DateTime dateMin, DateTime dateMax, int idclient)
        {
            IList<int> result = new List<int>();
            // statusId = 2  = transaction end   +  isReturn = false  = is not a Transtacion return
            IList<TRANSACTIONS> transacs = db.TRANSACTIONSs.Where(d => d.transactionDateEnd >= dateMin && d.transactionDateEnd <= dateMax && d.customerId == idclient && d.statusId == 2 && d.isReturn == false).ToList();
            foreach (var item in transacs)
            {
                result.Add(item.idTransaction);
            }
            return result;
        }

        public IList<int> GetTicket(DateTime dateMin, DateTime dateMax, decimal total)
        {
            IList<int> result = new List<int>();
            // statusId = 2  = transaction end   +  isReturn = false  = is not a Transtacion return
            IList<TRANSACTIONS> transacs = db.TRANSACTIONSs.Where(d => d.transactionDateEnd >= dateMin && d.transactionDateEnd <= dateMax && d.total == total && d.statusId == 2 && d.isReturn == false).ToList();
            foreach (var item in transacs)
            {
                result.Add(item.idTransaction);
            }
            return result;
        }

        public IList<int> GetTicket(int idLanguage)
        {
            List<int> result = db.TRANSACTIONS_MESSAGEs.Where(m => m.languageMessage == idLanguage).Select(t => t.transactionId).ToList();
            return result;
        }

        public int GetLanguageTicketByIdTransac(int idTransac)
        {
            return db.TRANSACTIONS_MESSAGEs.Where(m => m.transactionId == idTransac).Select(l => l.languageMessage).Single();
        }

        public IList<int> GetTicketNbItem(DateTime dateMin, DateTime dateMax, int nbItem)
        {  
            IList<int?> res = new List<int?>();
            IList<int> result = new List<int>();
            res = db.SPP_TicketNbItem(dateMin, dateMax, nbItem).ToList();
            for (int i = 0; i < res.Count(); i++)
            {
                result.Add(res[i].Value);
            }
            return result;
        }

        //public IList<int> GetTicket(DateTime dateMin, DateTime dateMax, int nbItem, int idLanguage, int idMethodP)
        //{
        //    IList<int> result = new List<int>();
        //    // statusId = 2  = transaction end   +  isReturn = false  = is not a Transtacion return
        //    IList<int> transacs = GetTicket(dateMin, dateMax);

        //    return result;
        //}

        //public IList<int> GetTicket(DateTime dateMin, DateTime dateMax, decimal total, int nbItem, int idLanguage, int idMethodP)
        //{
        //    IList<int> result = new List<int>();
        //    // statusId = 2  = transaction end   +  isReturn = false  = is not a Transtacion return
        //    IList<int> transacs = GetTicket(dateMin, dateMax, total);

        //    return result;
        //}


    }
}



