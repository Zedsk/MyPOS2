using MyPOS2.BL;
using MyPOS2.Models;
using MyPOS2.Models.management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyPOS2.Controllers
{
    [Authorize]
    public class UserInfosController : Controller
    {
        // GET: UserInfos
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index()
        {
            UserInfoViewModel vm = new UserInfoViewModel();
            vm.UserList = UserBL.FindAllUserInfo();
            return View(vm);
        }

        // GET: UserInfos
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = id;
            UserInfoViewModel vm = new UserInfoViewModel();
            vm.UserInfo = UserBL.FindUserInfoById(id);
            vm.Roles = UserBL.FindAllRole();
            return View(vm);
            //return View("Index", "Manage", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserInfoViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserBL.ModifyUserInfo(vmodel.UserInfo);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //to do insert to log file
                    var e1 = ex.GetBaseException(); // --> log
                    var e4 = ex.Message; // --> log
                    var e5 = ex.Source; // --> log
                    var e8 = ex.GetType(); // --> log
                    var e9 = ex.GetType().Name; // --> log

                    return View("Error");
                }
            }
            vmodel.Roles = UserBL.FindAllRole();
            return View(vmodel);
        }
    }
}