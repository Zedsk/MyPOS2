using System;
using System.Collections.Generic;
using System.Configuration;
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

        internal static IList<LANGUAGES> FindLanguageListWithoutUniversal()
        {
            using (IDalLanguage dal = new DalLanguage())
            {
                return dal.GetAllLanguageWithoutUniversal();
            }
        }

        internal static int FindIdLanguageByShortForm(string language)
        {
            using (IDalLanguage dal = new DalLanguage())
            {
                return dal.GetIdLanguageByShortForm(language.ToLower());
            }
        }

        internal static int CheckLanguageSession()
        {
            if (HttpContext.Current.Session["Language"] == null)
            {
                HttpContext.Current.Session["Language"] = ConfigurationManager.AppSettings["Language"];
            }
            string language = HttpContext.Current.Session["Language"].ToString();
            int lang;
            if (int.TryParse(language, out int codeL))
            {
                lang = codeL;
            }
            else
            {
                lang = FindIdLanguageByShortForm(language);
            }
            return lang;
        }

        internal static IList<LANGUAGES> KeepOnlyUniversal(IList<LANGUAGES> lang)
        {
            for (int i = 0; i < lang.Count(); i++)
            {
                if (lang[i].shortForm != "all")
                {
                    lang.Remove(lang[i]);
                }
            }
            return lang;
        }

        internal static int FindUniversalId()
        {
            using (IDalLanguage dal = new DalLanguage())
            {
                // to do  -> améliorer 
                string shortForm = "all";
                return dal.GetIdLanguageByShortForm(shortForm.ToLower());
            }
        }
    }
}