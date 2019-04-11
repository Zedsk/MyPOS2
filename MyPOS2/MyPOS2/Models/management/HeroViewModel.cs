using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.management
{
    public class HeroViewModel
    {
        //public string IdHero { get; set; }

        [Display(Name = "Image du héro")]
        public string ImageHero { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nom du héro")]
        public string NameHero { get; set; }

        public HERO Hero { get; set; }

        public HERO_TRANSLATION HeroTrans { get; set; }

        public IList<HERO_TRANSLATION> HeroesTrans { get; set; }

        [Display(Name = "Langue")]
        public string LanguageId { get; set; }

        public IList<LANGUAGES> ListLang { get; set; }

    }
}