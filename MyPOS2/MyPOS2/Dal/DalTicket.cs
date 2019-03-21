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

        public string GetTicketMessageByIdAndLanguage(int messageId, int languageMessage)
        {
            var ticket = db.TICKET_MESSAGEs.Where(t => t.idMessage == messageId && t.languageId == languageMessage).Single();
            return ticket.message;
        }

    }
}