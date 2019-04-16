using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Dal
{
    public class DalMessage : IDalMessage
    {
        #region DB

        private Pos1Entities db;

        public DalMessage()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public IList<SPP_MessageTrans_Result> GetAllMessageTrans()
        {
            return db.SPP_MessageTrans().ToList();
        }

    }
}