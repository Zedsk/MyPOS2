using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Dal;
using MyPOS2.Data.Entity;

namespace MyPOS2.BL
{
    public class LanguageBL
    {
        internal static IList<LANGUAGES> FindLanguageList()
        {
            using (IDalLanguage dal = new DalLanguage())
            {
                return dal.GetAllLanguage();
            }
        }

        internal static int FindIdLanguageByShortForm(string language)
        {
            using (IDalLanguage dal = new DalLanguage())
            {
                return dal.GetIdLanguageByShortForm(language.ToLower());
            }
        }
    }
}