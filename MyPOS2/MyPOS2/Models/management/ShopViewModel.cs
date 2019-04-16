using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.management
{
    public class ShopViewModel
    {
        [Display(Name = "Logo du magasin")]
        public string LogoShop { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nom du magasin")]
        public string NameShop { get; set; }

        [Display(Name = "Rue")]
        public string Street { get; set; }
        
        [Display(Name = "Code postal")]
        public string ZipCode { get; set; }

        [Display(Name = "Ville")]
        public string City { get; set; }

        [Display(Name = "Heures d'ouverture")]
        public string Opening { get; set; }

        [Display(Name = "Tel.")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public SHOP Shop { get; set; }

        public SHOP_TRANSLATION ShopTrans { get; set; }

        public IList<SHOP_TRANSLATION> ShopsTrans { get; set; }

        [Display(Name = "Langue")]
        public string LanguageId { get; set; }

        public IList<LANGUAGES> ListLang { get; set; }
    }
}