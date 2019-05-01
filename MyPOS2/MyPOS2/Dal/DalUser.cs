using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public IList<SPP_UserInfo_Role_Result> GetALLUserInfo()
        {
            return db.SPP_UserInfo_Role().ToList();
        }

        public SPP_UserInfo_Role_Result GetUserInfoById(string aspId)
        {
            return db.SPP_UserInfo_Role().Where(m => m.userId == aspId).Single();
        }

        public string GetRoleByAspId(string aspId)
        {
            return db.SPP_UserInfo_Role().Where(m => m.userId == aspId).Select(r => r.role).Single(); ;
        }

        public IList<AspNetRoles> GetAllRole()
        {
            return db.AspNetRoles.ToList();
        }

        public void UpdateUserInfo(USERINFO userInfo, string nameRole)
        {
            db.Entry(userInfo).State = EntityState.Modified;
            db.SPP_AspNetUserRoles_Update(userInfo.userId, nameRole);
            db.SaveChanges();
        }

        public string GetIdRoleByName(string nameRole)
        {
            return db.AspNetRoles.Where(r => r.Name == nameRole).Select(s => s.Id).ToString();
        }

        public void BlockUser(string id)
        {
            AspNetUsers user = db.AspNetUsers.Where(u => u.Id == id).Single();
            user.LockoutEnabled = false;
            db.SaveChanges();
        }

        public void UnBlockUser(string id)
        {
            AspNetUsers user = db.AspNetUsers.Where(u => u.Id == id).Single();
            user.LockoutEnabled = true;
            db.SaveChanges();
        }

        public AspNetUsers GetAspUserInfoById(string id)
        {
            return db.AspNetUsers.Where(u => u.Id == id).Single();
        }
    }
}