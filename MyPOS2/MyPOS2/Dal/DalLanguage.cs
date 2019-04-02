using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    public class DalLanguage : IDalLanguage
    {
        #region DB

        private Pos1Entities db;

        public DalLanguage()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public IList<LANGUAGES> GetAllLanguage()
        {
            return db.LANGUAGESs.ToList();
        }

        public IList<LANGUAGES> GetAllLanguageWithoutUniversal()
        {
            return db.LANGUAGESs.Where(l => l.shortForm != "all").ToList();
        }

        public int GetIdLanguageByShortForm(string lang)
        {
            return db.LANGUAGESs.Where(l => l.shortForm == lang).Select(t => t.idLanguage).Single();
        }
    }
}