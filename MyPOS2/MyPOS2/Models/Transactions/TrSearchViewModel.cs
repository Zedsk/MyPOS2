﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Models.Transactions
{
    public class TrSearchViewModel
    {
        public string Product { get; set; }

        public IList<PRODUCT> Products { get; set; }

        public string Price { get; set; }

        public string Image { get; set; }

        public string Method { get; set; }

        public string Argument { get; set; }
        
        public IList<BRAND> Brands { get; set; }

        public IList<HERO> Heros { get; set; }

        public IList<AGE> Ages { get; set; }

        //public IList<CATEGORY> Cats { get; set; }
        public IList<SPP_ParentCategories_Result> Cats { get; set; }

        public IList<SPP_ChildCategories_Result> CatsChild { get; set; }
        
    }
}