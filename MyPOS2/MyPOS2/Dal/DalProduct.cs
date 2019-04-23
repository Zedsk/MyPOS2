using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    public class DalProduct : IDalProduct
    {
        #region DB

        private Pos1Entities db;

        public DalProduct()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public PRODUCT GetProductByCode(string codeProduct)
        {
            return db.PRODUCTs.Where(p => p.barcode == codeProduct).Single();
        }

        public PRODUCT GetProductByName(string product)
        {
            throw new NotImplementedException();
        }

        public List<SPP_ProductTrans_Result> GetAllProductByCode(string codeProduct, int language)
        {
            List<SPP_ProductTrans_Result> productList = new List<SPP_ProductTrans_Result>();
            productList = db.SPP_ProductTrans(language).Where(p => p.barcode.Contains(codeProduct)).ToList();
            return productList;
        }

        public List<SPP_ProductTrans_Result> GetAllProductByName(string codeProduct, int language)
        {
            List<SPP_ProductTrans_Result> productList = new List<SPP_ProductTrans_Result>();
            productList = db.SPP_ProductTrans(language).Where(p => p.nameProduct.Contains(codeProduct)).ToList();
            return productList;
        }

        public string GetNameProductById(int idProduct, int language)
        {
            PRODUCT_TRANSLATION prod = db.PRODUCT_TRANSLATIONs.Where(p => p.productId == idProduct && (p.languageId == language || p.LANGUAGES.shortForm == "all")).Single();
            return prod.nameProduct;
        }

        public IList<SPP_ProductTrans_Result> GetAllProduct(int language)
        {
            List<SPP_ProductTrans_Result> productList = new List<SPP_ProductTrans_Result>();
            productList = db.SPP_ProductTrans(language).ToList();
            return productList;
        }

        public string GetCodeProductById(int id)
        {
            PRODUCT prod = db.PRODUCTs.Where(p => p.idProduct == id).Single();
            return prod.barcode;
        }

       
    }
}