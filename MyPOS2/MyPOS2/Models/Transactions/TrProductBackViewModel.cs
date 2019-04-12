using MyPOS2.Data.Entity;
using MyPOS2.Models.Transactions.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.Transactions
{
    public class TrProductBackViewModel
    {
        public TrTicketViewModel Ticket { get; set; }

        public string NumTransaction { get; set; }

        public string DateT { get; set; }

        public string Language { get; set; }

        public IList<LANGUAGES> Languages { get; set; }
    }
}