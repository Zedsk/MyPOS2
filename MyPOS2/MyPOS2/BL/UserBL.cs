using MyPOS2.Dal;
using MyPOS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class UserBL
    {
        internal static string FindNameByAspId(string userId)
        {
            using (IDalUser dal = new DalUser())
            {
                return dal.GetNameByAspId(userId);
            }
        }

        internal static string FindFirstnameByAspId(string userId)
        {
            using (IDalUser dal = new DalUser())
            {
                return dal.GetFirstnameByAspId(userId);
            }
        }

        internal static string FindStreetByAspId(string userId)
        {
            using (IDalUser dal = new DalUser())
            {
                return dal.GetStreetByAspId(userId);
            }
        }

        internal static string FindZipcodeByAspId(string userId)
        {
            using (IDalUser dal = new DalUser())
            {
                return dal.GetZipcodeByAspId(userId);
            }
        }

        internal static string FindCityByAspId(string userId)
        {
            using (IDalUser dal = new DalUser())
            {
                return dal.GetCityByAspId(userId);
            }
        }

        internal static string FindPhoneByAspId(string userId)
        {
            using (IDalUser dal = new DalUser())
            {
                return dal.GetPhoneByAspId(userId);
            }
        }

        internal static string FindEmailByAspId(string userId)
        {
            using (IDalUser dal = new DalUser())
            {
                return dal.GetEmailByAspId(userId);
            }
        }

        internal static void AddUserInfo(string idUser, RegisterViewModel model)
        {
            using (IDalUser dal = new DalUser())
            {
                dal.CreateUserInfo(idUser, model.Name, model.Firstname, model.Street, model.Zipcode, model.City, model.Phone);
            }
        }
    }
}