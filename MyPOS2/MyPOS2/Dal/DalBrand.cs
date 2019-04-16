using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Dal
{
    public class DalBrand : IDalBrand
    {
        #region DB

        private Pos1Entities db;

        public DalBrand()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public IList<BRAND> GetAllBrand()
        {
            return db.BRANDs.ToList();
        }
    }
}