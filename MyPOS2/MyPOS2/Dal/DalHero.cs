using MyPOS2.Data.Entity;
using MyPOS2.Models.management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Dal
{
    public class DalHero : IDalHero
    {
        #region DB

        private Pos1Entities db;

        public DalHero()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public IList<SPP_HeroesTrans_Result> GetAllHeroTrans()
        {
            return db.SPP_HeroesTrans().ToList();
        }
    }
}