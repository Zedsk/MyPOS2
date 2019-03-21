using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Dal
{
    public class DalUser : IDalUser
    {
        #region DB

        private Pos1Entities db;

        public DalUser()
        {
            db = new Pos1Entities();
        }
               
        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public string GetCityByAspId(string userId)
        {
            USERINFO user = db.USERINFOs.Where(v => v.userId == userId).Single();
            return user.city;
        }

        public string GetEmailByAspId(string userId)
        {
            //email = userName in dbo.AspNetUsers
            AspNetUsers user = db.AspNetUsers.Where(v => v.Id == userId).Single();
            return user.UserName;
        }

        public string GetFirstnameByAspId(string userId)
        {
            USERINFO user = db.USERINFOs.Where(v => v.userId == userId).Single();
            return user.firstname;
        }

        public string GetNameByAspId(string userId)
        {
            USERINFO user = db.USERINFOs.Where(v => v.userId == userId).Single();
            return user.nameUser;
        }

        public string GetPhoneByAspId(string userId)
        {
            USERINFO user = db.USERINFOs.Where(v => v.userId == userId).Single();
            return user.phone;
        }

        public string GetStreetByAspId(string userId)
        {
            USERINFO user = db.USERINFOs.Where(v => v.userId == userId).Single();
            return user.street;
        }

        public string GetZipcodeByAspId(string userId)
        {
            USERINFO user = db.USERINFOs.Where(v => v.userId == userId).Single();
            return user.zipCode;
        }

        public void CreateUserInfo(string idUser, string name, string firstname, string street, string zipcode, string city, string phone)
        {
            
            USERINFO info = new USERINFO {userId = idUser, nameUser = name, firstname = firstname, street = street, zipCode = zipcode, city = city, phone = phone, creationDate = DateTime.Now};
            db.USERINFOs.Add(info);
            db.SaveChanges();
        }
    }
}