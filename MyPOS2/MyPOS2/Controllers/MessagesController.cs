using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPOS2.BL;
using MyPOS2.Data.Entity;
using MyPOS2.Models.management;

namespace MyPOS2.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Messages
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            string nameSetting = "MessageGen";
            string messageG = SettingBL.FindSettingValueByName(nameSetting);
            ViewBag.messageG = int.Parse(messageG);
            int lang = LanguageBL.CheckLanguageSession();
            var messagesT = db.SPP_MessageTransDistinct(lang).ToList();

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!String.IsNullOrEmpty((searchString)))
            {
                messagesT = messagesT.Where(s => s.title.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    messagesT = messagesT.OrderByDescending(d => d.title).ToList();
                    break;
                default:
                    messagesT = messagesT.OrderBy(d => d.title).ToList();
                    break;
            }

            return View(messagesT);
        }

        // GET: Messages/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IList<SPP_MessageTrans_Result> messages = db.SPP_MessageTrans().Where(m => m.idMessage == id).ToList();
            return View(messages);
        }

        // GET: Messages/Create
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            MessageViewModel vm = new MessageViewModel
            {
                ListLang = LanguageBL.FindLanguageListWithoutUniversal()
            };
            return View(vm);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //Check if  min  1 value
                bool messageIsValid = TranslationBL.CheckIfMinOneValue(vmodel.MessagesTrans);
                if (messageIsValid)
                {
                    //Check if Message exist by title before create
                    bool titleExist = TranslationBL.CheckIfNameExist(vmodel.MessagesTrans);
                    if (!titleExist)
                    {
                        MESSAGE message = new MESSAGE();
                        db.MESSAGEs.Add(message);
                        int id = message.idMessage;
                        //Check if isUniversal
                        IList<MESSAGE_TRANSLATION> messagesT = TranslationBL.VerifyIsUniversal(vmodel.MessagesTrans, id);
                        db.MESSAGE_TRANSLATIONs.AddRange(messagesT);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //to do --> match the error with the name that causes the error
                        ViewBag.nameIsNotValid = "Le Titre existe déjà, veuillez saisir un autre nom!";
                    }
                }
                else
                {
                    ViewBag.nameIsNotValid = "Veuillez saisir un titre et un message!";
                }
                
            }
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            return View(vmodel);
        }

        // GET: Messages/Edit/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<MESSAGE_TRANSLATION> messageList = db.MESSAGE_TRANSLATIONs.Where(m => m.messageId == id).ToList();
            if (messageList.Count() == 0)
            {
                return HttpNotFound();
            }

            MessageViewModel vm = new MessageViewModel();
            bool isUniversal = false;
            if (messageList.Count() == 1)
            {
                isUniversal = true;
            }
            ViewBag.isUniversal = isUniversal;
            vm.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            vm.MessagesTrans = messageList;
            vm.IdMessage = id.Value;
            return View(vm);
        }

        // POST: Messages/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MessageViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                IList<MESSAGE_TRANSLATION> messagesT = TranslationBL.VerifyIsUniversal(vmodel.MessagesTrans, vmodel.IdMessage);
                foreach (var item in messagesT)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            return View(vmodel);
        }

        // GET: Messages/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int lang = LanguageBL.CheckLanguageSession();

            SPP_MessageTransDistinct_Result messages = db.SPP_MessageTransDistinct(lang).Where(m => m.idMessage == id).Single();
            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MESSAGE mESSAGE = db.MESSAGEs.Find(id);
            db.MESSAGEs.Remove(mESSAGE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Default(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //to do --> modify  nameSet
            string nameSet = "MessageGen";
            var result = db.SETTINGs.SingleOrDefault(s => s.nameSetting == nameSet);
            if (result != null)
            {
                result.valueSetting = id.ToString();
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
