using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    interface IDalUser : IDisposable
    {
        string GetNameByAspId(string userId);
        string GetFirstnameByAspId(string userId);
        string GetStreetByAspId(string userId);
        string GetZipcodeByAspId(string userId);
        string GetCityByAspId(string userId);
        string GetPhoneByAspId(string userId);
        string GetEmailByAspId(string userId);
        void CreateUserInfo(string idUser, string name, string firstname, string street, string zipcode, string city, string phone);
        IList<SPP_UserInfo_Role_Result> GetALLUserInfo();
        string GetRoleByAspId(string aspId);
        SPP_UserInfo_Role_Result GetUserInfoById(string aspId);
        IList<AspNetRoles> GetAllRole();
        void UpdateUserInfo(USERINFO userInfo, string role);
        string GetIdRoleByName(string nameRole);
        void BlockUser(string id);
        void UnBlockUser(string id);
        AspNetUsers GetAspUserInfoById(string id);
    }
}
