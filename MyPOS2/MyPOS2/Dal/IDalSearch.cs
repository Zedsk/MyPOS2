﻿using System;
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
        IList<SPP_ProductTrans_Result> GetAllProductByIdBrand(int id, int language);
        IList<HERO> GetAllHeros();
        IList<PRODUCT> GetAllProductByIdHero(int id);
        IList<SPP_ProductTrans_Result> GetAllProductByIdHero(int id, int language);
        IList<AGE> GetAllAges();
        IList<PRODUCT> GetAllProductByIdAge(int id);
        IList<SPP_ProductTrans_Result> GetAllProductByIdAge(int id, int language);
        IList<CATEGORY> GetAllCats();
        IList<SPP_ParentCategoriesTransDistinct_Result> GetAllCats(int language);
        IList<SPP_ChildCategoriesOfParent_Result> GetAllCats(int id, int language);
        IList<PRODUCT> GetAllProductByIdCat(int id);
        IList<SPP_ProductTrans_Result> GetAllProductByIdCat(int id, int language);
    }
}
