using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    interface IDalSearch : IDisposable
    {
        IList<BRAND> GetAllBrands();
        IList<PRODUCT> GetAllProductByIdBrand(int id);
        IList<HERO> GetAllHeros();
        IList<PRODUCT> GetAllProductByIdHero(int id);
        IList<AGE> GetAllAges();
        IList<PRODUCT> GetAllProductByIdAge(int id);
        IList<CATEGORY> GetAllCats();
        IList<SPP_ParentCategories_Result> GetAllCats(int language);
        IList<SPP_ChildCategories_Result> GetAllCats(int id, int language);
        IList<PRODUCT> GetAllProductByIdCat(int id);
    }
}
