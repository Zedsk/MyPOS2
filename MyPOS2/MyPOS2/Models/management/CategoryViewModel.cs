using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.management
{
    public class CategoryViewModel
    {
        public int IdCat { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Nom")]
        public string NameCat { get; set; }

        [Display(Name = "Type")]
        public string Relation { get; set; }

        [Display(Name = "Catégorie parent")]
        public int? Parent { get; set; }

        //public IList<CATEGORY> Parents { get; set; }

        //[Display(Name = "Catégorie enfant")]
        //public CATEGORY Child { get; set; }

        //public IList<CATEGORY> Children { get; set; }

        //public IList<CategoryViewModel> ListCatVM { get; set; }

        public IList<SPP_CategoryTransDistinct_Result> Categories { get; set; }

        public IList<SPP_CategoryTrans_Result> CatsTr { get; set; }

        public CATEGORY Cat { get; set; }

        public IList<CATEGORY_TRANSLATION> CategoriesTrans { get; set; }

        [Display(Name = "Langue")]
        public string LanguageId { get; set; }

        public IList<LANGUAGES> ListLang { get; set; }

        readonly List<string> RelationCat = new List<string> { "Parent", "Parent/Child", "Child" };


    }
}