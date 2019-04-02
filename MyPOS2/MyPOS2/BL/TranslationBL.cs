using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}