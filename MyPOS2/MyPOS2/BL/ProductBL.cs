using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class ProductBL
    {
        internal static PRODUCT FindProductByCode(string codeProduct)
        {
            using (IDalProduct dal = new DalProduct())
            {
                return dal.GetProductByCode(codeProduct);
            }
        }

        internal static List<SPP_ProductTrans_Result> FindAllProductByCode(string codeProduct, string language)
        {
            using (IDalProduct dal = new DalProduct())
            {
                int lang;
                if (int.TryParse(language, out int codeL))
                {
                    lang = codeL;
                }
                else
                {
                    lang = LanguageBL.FindIdLanguageByShortForm(language);
                }
                return dal.GetAllProductByCode(codeProduct, lang);
            }
        }

        internal static object FindProductByName(string product)
        {
            using (IDalProduct dal = new DalProduct())
            {
                return dal.GetProductByName(product);
            }
        }

        internal static List<SPP_ProductTrans_Result> FindAllProductByName(string product, string language)
        {
            using (IDalProduct dal = new DalProduct())
            {
                int lang;
                if (int.TryParse(language, out int codeL))
                {
                    lang = codeL;
                }
                else
                {
                    lang = LanguageBL.FindIdLanguageByShortForm(language);
                }
                return dal.GetAllProductByName(product, lang);
            }
        }

        internal static IList<SPP_ProductTrans_Result> FindAllProduct(string language)
        {
            using (IDalProduct dal = new DalProduct())
            {
                int lang;
                if (int.TryParse(language, out int codeL))
                {
                    lang = codeL;
                }
                else
                {
                    lang = LanguageBL.FindIdLanguageByShortForm(language);
                }
                return dal.GetAllProduct(lang);
            }
        }

        internal static string FindCodeProductById(int id)
        {
            using (IDalProduct dal = new DalProduct())
            {
                return dal.GetCodeProductById(id);
            }
        }
    }
}