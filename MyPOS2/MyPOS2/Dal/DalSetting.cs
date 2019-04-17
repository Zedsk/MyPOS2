using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Dal
{
    public class DalSetting : IDalSetting
    {
        #region DB

        private Pos1Entities db;

        public DalSetting()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        #endregion

        public IList<SETTING> GetAllSetting()
        {
            return db.SETTINGs.ToList();
        }

        public SETTING GetSettingValueByName(string name)
        {
            return db.SETTINGs.Where(s => s.nameSetting == name).Single();
        }
    }
}