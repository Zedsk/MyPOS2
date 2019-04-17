using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class SearchBL
    {
        internal static IList<BRAND> FindBrandsList()
        {
            using (IDalSearch dal = new DalSearch())
            {
                return dal.GetAllBrands();
            }
        }

        //internal static IList<PRODUCT> FindProductListByIdBrand(string argument)
        //{
        //    using (IDalSearch dal = new DalSearch())
        //    {
        //        int id = int.Parse(argument);
        //        return dal.GetAllProductByIdBrand(id);
        //    }
        //}

        internal static IList<SPP_ProductTrans_Result> FindProductListByIdBrand(string argument, string language)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                //find idLanguage by shortForm
                int lang = LanguageBL.FindIdLanguageByShortForm(language);
                return dal.GetAllProductByIdBrand(id, lang);
            }
        }

        
        internal static IList<HERO> FindHerosList()
        {
            using (IDalSearch dal = new DalSearch())
            {
                return dal.GetAllHeros();
            }
        }

        //internal static IList<PRODUCT> FindProductListByIdHero(string argument)
        //{
        //    using (IDalSearch dal = new DalSearch())
        //    {
        //        int id = int.Parse(argument);
        //        return dal.GetAllProductByIdHero(id);
        //    }
        //}

        internal static IList<SPP_ProductTrans_Result> FindProductListByIdHero(string argument, string language)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                //find idLanguage by shortForm
                int lang = LanguageBL.FindIdLanguageByShortForm(language);
                return dal.GetAllProductByIdHero(id, lang);
            }
        }

        internal static IList<AGE> FindAgesList()
        {
            using (IDalSearch dal = new DalSearch())
            {
                return dal.GetAllAges();
            }
        }

        //internal static IList<PRODUCT> FindProductListByIdAge(string argument)
        //{
        //    using (IDalSearch dal = new DalSearch())
        //    {
        //        int id = int.Parse(argument);
        //        return dal.GetAllProductByIdAge(id);
        //    }
        //}

        internal static IList<SPP_ProductTrans_Result> FindProductListByIdAge(string argument, string language)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                //find idLanguage by shortForm
                int lang = LanguageBL.FindIdLanguageByShortForm(language);
                return dal.GetAllProductByIdAge(id, lang);
            }
        }

        internal static IList<CATEGORY> FindCatsParentList()
        {
            using (IDalSearch dal = new DalSearch())
            {
                return dal.GetAllCats();
            }
        }

        internal static IList<SPP_ParentCategoriesTransDistinct_Result> FindCatsParentList(string language)
        {
            using (IDalSearch dal = new DalSearch())
            {
                //find idLanguage by shortForm
                int lang = LanguageBL.FindIdLanguageByShortForm(language);
                return dal.GetAllCats(lang);
            }
        }

        internal static IList<SPP_ChildCategoriesOfParent_Result> FindCatsChildList(string argument, string language)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                //find idLanguage by shortForm
                int lang = LanguageBL.FindIdLanguageByShortForm(language);
                return dal.GetAllCats(id, lang);
            }
        }

        //internal static IList<PRODUCT> FindProductListByIdCat(string argument)
        //{
        //    using (IDalSearch dal = new DalSearch())
        //    {
        //        int id = int.Parse(argument);
        //        return dal.GetAllProductByIdCat(id);
        //    }
        //}

        internal static IList<SPP_ProductTrans_Result> FindProductListByIdCat(string argument, string language)
        {
            using (IDalSearch dal = new DalSearch())
            {
                int id = int.Parse(argument);
                //find idLanguage by shortForm
                int lang = LanguageBL.FindIdLanguageByShortForm(language);
                return dal.GetAllProductByIdCat(id, lang);
            }
        }

    }
}