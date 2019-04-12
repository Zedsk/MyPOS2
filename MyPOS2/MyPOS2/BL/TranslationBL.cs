﻿using System;
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
            bool result = true;
            List<string> nameList = new List<string>();
            foreach (var item in shopsT)
            {
                if (item.nameShop != null)
                {
                    nameList.Add(item.nameShop);
                }
            }
            if (nameList.Count() > 1)
            {
                result = false;
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
    }
}