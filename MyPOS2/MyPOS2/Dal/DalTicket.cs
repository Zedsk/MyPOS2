using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

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
        //    var ticket = db.TICKET_MESSAGEs.Where(t => t.idMessage == messageId && t.languageId == languageMessage).Single();
        //    return ticket.message;
        //}

        public string GetTicketMessageByIdAndLanguage(int idMessage, int languageMessage)
        {
            var ticket = db.MESSAGE_TRANSLATIONs.Where(t => t.messageId == idMessage && t.languageId == languageMessage).Single();
            return ticket.message;
        }

        public List<string> GetListTicketMessageByIdAndLanguage(List<int?> idMessage, int languageMessage)
        {
            List<string> result = new List<string>();
            List<MESSAGE_TRANSLATION> listMessages = db.MESSAGE_TRANSLATIONs.Where(m => m.languageId == languageMessage).ToList();
            for (int i = 0; i < idMessage.Count(); i++)
            {
                result.Add(listMessages.Find(m => m.messageId == idMessage[i] && m.languageId == languageMessage).message);
            }
            return result;
        }

        public List<int?> GetListMessagesId(int transacId)
        {
            List<int?> messageIds = new List<int?>();
            messageIds = db.SPP_TransactionMessageIds(transacId).ToList();
            return messageIds;
        }

        public void UpdateTransactionMessage(int transacId, int messageId)
        {
            TICKET_MESSAGE tMessage = db.TICKET_MESSAGEs.FirstOrDefault(m => m.idMessage == messageId);
            db.TRANSACTIONSs.FirstOrDefault(t => t.idTransaction == transacId).TICKET_MESSAGE.Add(tMessage);
            db.SaveChanges();
        }
    }
}