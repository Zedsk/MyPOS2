using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    public class DalSearch : IDalSearch
    {
        #region DB

        private Pos1Entities db;

        public DalSearch()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public IList<BRAND> GetAllBrands()
        {
            return db.BRANDs.ToList();
        }

        public IList<PRODUCT> GetAllProductByIdBrand(int id)
        {
            List<PRODUCT> productList = new List<PRODUCT>();
            productList = db.PRODUCTs.Where(p => p.brandId == id).ToList();
            return productList;
        }

        public IList<SPP_ProductTrans_Result> GetAllProductByIdBrand(int id, int language)
        {
            List<SPP_ProductTrans_Result> productList = new List<SPP_ProductTrans_Result>();
            productList = db.SPP_ProductTrans(language).Where(p => p.brandId == id).ToList();
            return productList;
        }

        public IList<HERO> GetAllHeros()
        {
            return db.HEROs.ToList();
        }

        public IList<PRODUCT> GetAllProductByIdHero(int id)
        {
            List<PRODUCT> productList = new List<PRODUCT>();
            productList = db.PRODUCTs.Where(p => p.heroId == id).ToList();
            return productList;
        }

        public IList<SPP_ProductTrans_Result> GetAllProductByIdHero(int id, int language)
        {
            List<SPP_ProductTrans_Result> productList = new List<SPP_ProductTrans_Result>();
            productList = db.SPP_ProductTrans(language).Where(p => p.heroId == id).ToList();
            return productList;
        }

        public IList<AGE> GetAllAges()
        {
            return db.AGEs.ToList();
        }

        public IList<PRODUCT> GetAllProductByIdAge(int id)
        {
            List<PRODUCT> productList = new List<PRODUCT>();
            productList = db.PRODUCTs.Where(p => p.ageId == id).ToList();
            return productList;
        }

        public IList<SPP_ProductTrans_Result> GetAllProductByIdAge(int id, int language)
        {
            List<SPP_ProductTrans_Result> productList = new List<SPP_ProductTrans_Result>();
            productList = db.SPP_ProductTrans(language).Where(p => p.ageId == id).ToList();
            return productList;
        }

        public IList<CATEGORY> GetAllCats()
        {
            return db.CATEGORYs.ToList();
        }

        public IList<SPP_ParentCategoriesTransDistinct_Result> GetAllCats(int language)
        {
            //return all parents category            
            List<SPP_ParentCategoriesTransDistinct_Result> parents = null;
            parents = db.SPP_ParentCategoriesTransDistinct(language).ToList();
            return parents;
        }

        public IList<SPP_ChildCategoriesOfParent_Result> GetAllCats(int id, int language)
        {
            //return all children category            
            List<SPP_ChildCategoriesOfParent_Result> children = null;
            children = db.SPP_ChildCategoriesOfParent(id, language).ToList();
            return children;
        }

        public IList<PRODUCT> GetAllProductByIdCat(int id)
        {
            List<PRODUCT> productList = new List<PRODUCT>();
            productList = db.PRODUCTs.Where(p => p.categoryId == id).ToList();
            return productList;
        }

        public IList<SPP_ProductTrans_Result> GetAllProductByIdCat(int id, int language)
        {
            List<SPP_ProductTrans_Result> productList = new List<SPP_ProductTrans_Result>();
            productList = db.SPP_ProductTrans(language).Where(p => p.categoryId == id).ToList();
            return productList;
        }
    }
}