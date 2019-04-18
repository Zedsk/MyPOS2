using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.management
{
    public class UserInfoViewModel
    {
        public IList<SPP_UserInfo_Role_Result> UserList { get; set; }

        public SPP_UserInfo_Role_Result UserInfo { get; set; }

        public AspNetRoles Role { get; set; }

        public IList<AspNetRoles> Roles { get; set; }

        public bool HasPassword { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}