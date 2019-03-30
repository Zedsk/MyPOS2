using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS2.Dal
{
    interface IDalShop : IDisposable
    {
        List<SPP_GetShopById_Result> GetShopById(int id);
        List<SPP_GetAllShop_Result> GetALLShop();
    }
}
