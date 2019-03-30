using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class ShopBL
    {
        internal static SPP_GetShopById_Result FindShopById(int id, int language)
        {
            using (IDalShop dal = new DalShop())
            {
                List<SPP_GetShopById_Result> result = dal.GetShopById(id);
                SPP_GetShopById_Result newResult = result.Where(r => r.languageId == language).Single();
                return newResult;
            }
        }

        internal static List<SPP_GetShopById_Result> FindALLShop()
        {
            using (IDalShop dal = new DalShop())
            {
                List<SPP_GetShopById_Result> shops = new List<SPP_GetShopById_Result>();
                dal.GetALLShop();
                return shops;
            }
        }
    }
}