using MyPOS2.Data.Entity;
using MyPOS2.Models.management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS2.Dal
{
    interface IDalHero
    {
        IList<SPP_HeroesTrans_Result> GetAllHeroTrans();
    }
}
