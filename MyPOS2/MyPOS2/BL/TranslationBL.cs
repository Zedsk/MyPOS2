using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPOS2.Dal;
using MyPOS2.Data.Entity;

namespace MyPOS2.BL
{
    public class TranslationBL
    {
        internal static bool CheckIfUniversal(IList<HERO_TRANSLATION> heroesT)
        {
            bool result = true;
            List<string> nameList = new List<string>();
            foreach (var item in heroesT)
            {
                if (item.nameHero != null)
                {
                    nameList.Add(item.nameHero);
                }
            }
            if (nameList.Count() > 1)
            {
                result = false;
            }
            return result;
        }

        internal static bool CheckIfUniversal(IList<SHOP_TRANSLATION> shopsT)
        {
            bool result = false;
            List<string> nameList = new List<string>();
            List<string> streetList = new List<string>();
            List<string> cityList = new List<string>();
            List<string> opList = new List<string>();
            foreach (var item in shopsT)
            {
                if (item.nameShop != null)
                {
                    nameList.Add(item.nameShop);
                }
                if (item.street != null)
                {
                    streetList.Add(item.nameShop);
                }
                if (item.city != null)
                {
                    cityList.Add(item.nameShop);
                }
                if (item.opening != null)
                {
                    opList.Add(item.nameShop);
                }

            }
            if (nameList.Count() == 1 && streetList.Count() == 1 && cityList.Count() == 1 && opList.Count() == 1)
            {
                result = true;
            }
            return result;
        }

        internal static bool CheckIfUniversal(IList<PRODUCT_TRANSLATION> productsT)
        {
            bool result = false;
            List<string> nameList = new List<string>();
            List<string> descList = new List<string>();
            foreach (var item in productsT)
            {
                if (item.nameProduct != null)
                {
                    nameList.Add(item.nameProduct);
                }
                //if (item.description != null)
                //{
                //    descList.Add(item.description);
                //}
            }
            //if (nameList.Count() > 1 && descList.Count() > 1)
            if (nameList.Count() == 1)
            {
                result = true;
            }
            return result;
        }

        //// version générique à revoir à cause de item.name 
        //internal static bool CheckIfUniversal<T>(IList<T> lisT)
        //{
        //    bool result = true;
        //    List<string> nameList = new List<string>();
        //    foreach (var item in lisT)
        //    {
        //        if (item.name != null)
        //        {
        //            nameList.Add(item.name);
        //        }
        //    }
        //    if (nameList.Count() > 1)
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

        internal static bool CheckIfNameExist(IList<HERO_TRANSLATION> heroesT)
        {
            using (IDalHero dal = new DalHero())
            {
                bool result = false;
                List<string> nameList = dal.GetAllHeroTrans().Select(n => n.nameHero.ToLower()).ToList();
                foreach (var item in heroesT)
                {
                    if (nameList.Contains(item.nameHero.ToLower()))
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            }
        }
                
        internal static bool CheckIfNameHeroIsValid(IList<HERO_TRANSLATION> heroesT)
        {
            bool result = true;
            List<string> nameList = new List<string>();
            foreach (var item in heroesT)
            {
                if (item.nameHero != null)
                {
                    nameList.Add(item.nameHero);
                }
            }
            if (nameList.Count() == 0)
            {
                result = false;
            }
            return result;
        }

        internal static IList<PRODUCT_TRANSLATION> VerifyIsUniversal(IList<PRODUCT_TRANSLATION> productsTrans, int id)
        {
            bool isUniversal = TranslationBL.CheckIfUniversal(productsTrans);
            int count = productsTrans.Count();
            if (isUniversal)
            {
                List<PRODUCT_TRANSLATION> result = new List<PRODUCT_TRANSLATION>();
                productsTrans[0].productId = id;
                //change language with universal
                productsTrans[0].languageId = LanguageBL.FindUniversalId();
                if (productsTrans[0].description == null)
                {
                    productsTrans[0].description = " - ";
                }
                result.Add(productsTrans[0]);

                return result;
            }
            else
            {
                foreach (var item in productsTrans)
                {
                    item.productId = id;
                }
                return productsTrans;
            }
        }

        internal static IList<SHOP_TRANSLATION> VerifyIsUniversal(IList<SHOP_TRANSLATION> shopsTrans, int id)
        {
            bool isUniversal = TranslationBL.CheckIfUniversal(shopsTrans);
            int count = shopsTrans.Count();
            if (isUniversal)
            {
                List<SHOP_TRANSLATION> result = new List<SHOP_TRANSLATION>();
                shopsTrans[0].shopId = id;
                //change language with universal
                shopsTrans[0].languageId = LanguageBL.FindUniversalId();
                
                result.Add(shopsTrans[0]);

                return result;
            }
            else
            {
                foreach (var item in shopsTrans)
                {
                    item.shopId = id;
                }
                return shopsTrans;
            }
        }
    }
}