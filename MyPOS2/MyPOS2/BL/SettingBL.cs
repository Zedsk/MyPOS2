using MyPOS2.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class SettingBL
    {
        public string FindSettingValueByName(string name)
        {
            using (IDalSetting dal = new DalSetting())
            {
                return dal.GetSettingValueByName(name);
            }
        }
    }
}