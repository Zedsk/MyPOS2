using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    public class DalCategory :IDalCategory
    {
        #region DB

        private Pos1Entities db;

        public DalCategory()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion


        public IList<SPP_CategoryTrans_Result> GetAllCategoryTrans()
        {
            return db.SPP_CategoryTrans().ToList();
        }
    }
}