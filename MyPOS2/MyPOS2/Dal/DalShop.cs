using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Dal
{
    public class DalShop : IDalShop
    {
        #region DB

        private Pos1Entities db;

        public DalShop()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public List<SPP_GetShopById_Result> GetShopById(int id)
        {
            List<SPP_GetShopById_Result> result = new List<SPP_GetShopById_Result>();
            result = db.SPP_GetShopById(id).ToList();
            return result;
        }

        public List<SPP_GetAllShop_Result> GetALLShop()
        {
            List<SPP_GetAllShop_Result> shops = new List<SPP_GetAllShop_Result>();
            shops = db.SPP_GetAllShop().ToList();
            return shops;
        }
    }
}