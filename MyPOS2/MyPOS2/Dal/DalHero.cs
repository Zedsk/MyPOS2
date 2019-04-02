using MyPOS2.Data.Entity;
using MyPOS2.Models.management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Dal
{
    public class DalHero : IDalHero
    {
        #region DB

        private Pos1Entities db;

        public DalHero()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        //public List<HeroViewModel> GetAllHeroTrans()
        //{
        //    var list = db.HEROs.Join(db.HERO_TRANSLATIONs, (h => h.idHero), (ht => ht.heroId), (h, ht) => new { h.idHero, ht.nameHero, h.imageHero, ht.languageId }).ToList();
        //    List<HeroViewModel> result = new List<HeroViewModel>();
        //    for (int i = 0; i < list.Count(); i++)
        //    {
        //        HeroViewModel vm = new HeroViewModel
        //        {
        //            IdHero = list[i].idHero.ToString(),
        //            NameHero = list[i].nameHero,
        //            ImageHero = list[i].imageHero,
        //            LanguageId = list[i].languageId.ToString()
        //        };
        //        result.Add(vm);
        //    }
        //    return result;
        //}

        public IList<SPP_HeroesTrans_Result> GetAllHeroTrans()
        {
            return db.SPP_HeroesTrans().ToList();
        }
    }
}