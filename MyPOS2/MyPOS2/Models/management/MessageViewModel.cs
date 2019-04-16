using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.management
{
    public class MessageViewModel
    {
        public int IdMessage { get; set; }

        [Display(Name = "Titre")]
        public string Title { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Langue")]
        public string LanguageId { get; set; }

        public IList<LANGUAGES> ListLang { get; set; }

        public IList<MESSAGE_TRANSLATION> MessagesTrans { get; set; }

    }
}