using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS2.Dal
{
    interface IDalCategory : IDisposable
    {
        IList<SPP_CategoryTrans_Result> GetAllCategoryTrans();
    }
}
