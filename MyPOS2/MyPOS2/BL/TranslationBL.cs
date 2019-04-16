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
            //if (titleList.Count() > 1 && descList.Count() > 1)
            if (nameList.Count() == 1)
            {
                result = true;
            }
            return result;
        }

        

        private static bool CheckIfUniversal(IList<MESSAGE_TRANSLATION> messagesT)
        {
            bool result = false;
            List<string> nameList = new List<string>();
            foreach (var item in messagesT)
            {
                if (item.title != null)
                {
                    nameList.Add(item.title);
                }
            }
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
        //    List<string> titleList = new List<string>();
        //    foreach (var item in lisT)
        //    {
        //        if (item.name != null)
        //        {
        //            titleList.Add(item.name);
        //        }
        //    }
        //    if (titleList.Count() > 1)
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

        internal static bool CheckIfNameExist(IList<MESSAGE_TRANSLATION> messagesT)
        {
            using (IDalMessage dal = new DalMessage())
            {
                bool result = false;
                List<string> titleList = dal.GetAllMessageTrans().Select(n => n.title.ToLower()).ToList();
                foreach (var item in messagesT)
                {
                    if (item.title != null)
                    {
                        if (titleList.Contains(item.title.ToLower()))
                        {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }
        }

        internal static bool CheckIfNameExist(IList<CATEGORY_TRANSLATION> catsT)
        {
            using (IDalCategory dal = new DalCategory())
            {
                bool result = false;
                List<string> nameList = dal.GetAllCategoryTrans().Select(n => n.nameCategory.ToLower()).ToList();
                foreach (var item in catsT)
                {
                    if (nameList.Contains(item.nameCategory.ToLower()))
                    {
                        result = true;
                        break;
                    }
                }
                return result;
            }
        }

        internal static bool CheckIfNameExist(BRAND brand)
        {
            using (IDalBrand dal = new DalBrand())
            {
                bool result = false;
                List<string> nameList = dal.GetAllBrand().Select(n => n.nameBrand.ToLower()).ToList();
                if (nameList.Contains(brand.nameBrand.ToLower()))
                {
                    result = true;
                }
                return result;
            }
        }

        internal static bool CheckIfNameExist(SETTING setting)
        {
            using (IDalSetting dal = new DalSetting())
            {
                bool result = false;
                List<string> nameList = dal.GetAllSetting().Select(n => n.nameSetting.ToLower()).ToList();
                if (nameList.Contains(setting.nameSetting.ToLower()))
                {
                    result = true;
                }
                return result;
            }
        }

        internal static bool CheckIfMinOneValued(IList<HERO_TRANSLATION> heroesT)
        {
            //to do --> ameliorer !
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

        internal static bool CheckIfMinOneValue(IList<MESSAGE_TRANSLATION> messagesTrans)
        {
            //to do --> ameliorer !
            bool result = true;
            List<string> titleList = new List<string>();
            List<string> messageList = new List<string>();
            foreach (var item in messagesTrans)
            {
                if (item.title != null)
                {
                    titleList.Add(item.title);
                }
                if (item.message != null)
                {
                    messageList.Add(item.message);
                }
            }
            if (titleList.Count() == 0 || messageList.Count() == 0)
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

        internal static IList<MESSAGE_TRANSLATION> VerifyIsUniversal(IList<MESSAGE_TRANSLATION> messagesTrans, int id)
        {
            bool isUniversal = TranslationBL.CheckIfUniversal(messagesTrans);
            int count = messagesTrans.Count();
            if (isUniversal)
            {
                List<MESSAGE_TRANSLATION> result = new List<MESSAGE_TRANSLATION>();
                messagesTrans[0].messageId = id;
                //change language with universal
                messagesTrans[0].languageId = LanguageBL.FindUniversalId();

                result.Add(messagesTrans[0]);

                return result;
            }
            else
            {
                foreach (var item in messagesTrans)
                {
                    item.messageId = id;
                }
                return messagesTrans;
            }
        }

        
    }
}