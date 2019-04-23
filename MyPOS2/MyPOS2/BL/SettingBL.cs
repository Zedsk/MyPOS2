using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class SettingBL
    {
        internal static IList<SETTING> FindAllSetting()
        {
            using (IDalSetting dal = new DalSetting())
            {
                return dal.GetAllSetting();
            }
        }

        internal static string FindSettingValueByName(string name)
        {
            using (IDalSetting dal = new DalSetting())
            {
                SETTING val = dal.GetSettingValueByName(name);
                return val.valueSetting;
            }
        }
    }
}