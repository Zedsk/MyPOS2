using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    interface IDalProduct : IDisposable
    {
        PRODUCT GetProductByCode(string codeProduct);
        //List<PRODUCT> GetAllProductByCode(string codeProduct);
        List<SPP_ProductTrans_Result> GetAllProductByCode(string codeProduct, int language);
        PRODUCT GetProductByName(string product);
        List<SPP_ProductTrans_Result> GetAllProductByName(string codeProduct, int language);
        string GetNameProductById(int idProduct, int language);
    }
}
