using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.management
{
    public class ProductViewModel
    {
        [Display(Name = "Id")]
        public int IdProduct { get; set; }

        [Required]
        [Display(Name = "Code barre")]
        public string Barcode { get; set; }

        [Required]
        [Display(Name = "Prix")]
        public decimal Price { get; set; }

        [Display(Name = "Remise (%)")]
        [Range(0, 100, ErrorMessage = "valeur devant être comprise entre 0 et 100")]
        public decimal? Discount { get; set; }

        [Display(Name = "Image")]
        public string ImageProduct { get; set; }

        [Required]
        [Display(Name = "Catégorie")]
        public string Category { get; set; }

        public IList<CATEGORY_TRANSLATION> CategoriesTrans { get; set; }

        [Required]
        [Display(Name = "Age")]
        public string Age { get; set; }

        public IList<AGE> Ages { get; set; }

        [Required]
        [Display(Name = "Marque")]
        public string Brand { get; set; }

        public IList<BRAND> Brands { get; set; }

        [Display(Name = "Héro")]
        public string Hero { get; set; }

        public IList<HERO_TRANSLATION> HeroesTrans { get; set; }

        //[Display(Name = "TVA")]
        //public string Vat { get; set; }

        [Display(Name = "TVA")]
        public string Vat { get { return "2"; } }

        public IList<VAT> Vats { get; set; }

        public PRODUCT Product { get; set; }

        public PRODUCT_TRANSLATION ProductTrans { get; set; }

        public IList<PRODUCT_TRANSLATION> ProductsTrans { get; set; }

        [Display(Name = "Langue")]
        public string LanguageId { get; set; }

        public IList<LANGUAGES> ListLang { get; set; }
    }
}