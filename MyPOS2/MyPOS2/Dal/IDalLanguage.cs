using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS2.Dal
{
    interface IDalLanguage : IDisposable
    {
        IList<LANGUAGES> GetAllLanguage();
        IList<LANGUAGES> GetAllLanguageWithoutUniversal();
        int GetIdLanguageByShortForm(string lang);
    }
}
