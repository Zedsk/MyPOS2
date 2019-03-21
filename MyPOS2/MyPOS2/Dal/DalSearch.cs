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

        public IList<CATEGORY> GetAllCats()
        {
            return db.CATEGORYs.ToList();
        }

        public IList<PRODUCT> GetAllProductByIdCat(int id)
        {
            List<PRODUCT> productList = new List<PRODUCT>();
            productList = db.PRODUCTs.Where(p => p.categoryId == id).ToList();
            return productList;
        }

    }
}