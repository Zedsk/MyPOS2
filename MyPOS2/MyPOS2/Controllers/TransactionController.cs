using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core;
using Microsoft.AspNet.Identity;
using MyPOS2.BL;
using MyPOS2.Models.Transactions;
using System.Configuration;

namespace MyPOS2.Controllers
{
    public class TransactionController : Controller
    {
        #region Index
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                //récupération du terminal
                int terminal = 0;

                //var T = System.Net.Dns.GetHostName();

                if (Session["sessTerminalId"] != null)
                {
                    //terminal = int.Parse(Session["sessTerminalId"].ToString());
                    terminal = Convert.ToInt32(Session["sessTerminalId"]);
                }
                else
                {
                    terminal = TerminalBL.FindTerminalIdByDate();
                    // tot do --> modifier si plusieur cashday diff terminaux
                }

                ////récupération de la langue
                if (Session["Language"] == null)
                {
                    Session["Language"] = ConfigurationManager.AppSettings["Language"];
                }

                //string L = ConfigurationManager.AppSettings["Language"];
                //ConfigurationManager.AppSettings["Language"] = "Nl";
                //var L2 = ConfigurationManager.AppSettings["Language"];
                
                //récupération de l'utilisateur
                string currentId = User.Identity.GetUserId();
                TrIndexViewModel vm = new TrIndexViewModel
                {
                    ////terminal name or id
                    TerminalId = terminal,

                    ////transaction num = id
                    // to do --> provisoire vendorId = 1, shopId = 1, customerId = 1
                    NumTransaction = TransactionBL.InitializeNewTransaction(terminal, currentId),

                    // to do --> quid date et heure?
                    DateDay = DateTime.Now.Date.ToString("d"),
                    HourDay = DateTime.Now.ToString("T"),
                };
                return View(vm);
            }

            catch (EntityException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log
                TempData["Error"] = "L'initialisation de la transaction ne s'est pas déroulé correctement, veuillez contacter l'administrateur";
                return RedirectToAction("Transaction", "Home");
            }

            catch (InvalidOperationException ex)
            {
                //Viewbag not work with RedirectToAction --> use TempData
                //ViewBag.Error = "Il manque un fond de caisse pour cette date";
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log
                TempData["Error"] = "Il manque un fond de caisse pour cette date";
                return RedirectToAction("Transaction", "Home");
            }

            catch (Exception ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); //Description error --> log
                //var e2 = ex.GetType(); //InvalidOperationException //DbUpdateException
                //var e3 = ex.InnerException;
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                TempData["Error"] = "Il y a un problème avec l'action demandée, veuillez contacter l'administrateur";
                return RedirectToAction("Transaction", "Home");
            }


        }

        [HandleError]
        public ActionResult TransacBack(TrPaymentMenuViewModel vmodel)
        {
            try
            {
                var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
                var transac = TransactionBL.FindTransactionById(vmodel.NumTransaction);
                TrIndexViewModel vm = new TrIndexViewModel
                {
                    //vm.TerminalId = terminal;
                    NumTransaction = transac.idTransaction.ToString(),
                    TerminalId = transac.terminalId,
                    GlobalTot = transac.total.ToString(),
                    GlobalDiscount = (transac.discountGlobal) * 100,
                    // to do --> quid date et heure?
                    DateDay = DateTime.Now.Date.ToString("d"),
                    HourDay = DateTime.Now.ToString("T"),
                    //to do --> ameliorer   Vendor = string  et vendorId = int
                    Vendor = (transac.userId).ToString(),
                    //to do or not--> transac.discountGlobal à afficher
                    //to do or not--> with transac.vatId  return appliedVat
                    DetailsListWithTot = detailsListTot
                };
                //Sum subTotItems 
                ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                return View("Index", vm);
            }
            catch (InvalidOperationException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log
                //NumTransaction not valid
                //return RedirectToAction("Index", "Pay", numTransac);
                return RedirectToAction("Index", "Pay", vmodel);
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

        //POST:
        [HandleError]
        [HttpPost]
        public ActionResult Index(string submitButton, string globalDiscount, TrIndexViewModel vmodel)
        {
            try
            {
                switch (submitButton)
                {
                    case "Payment":
                        return (Payment(globalDiscount, vmodel));

                    case "Cancel":
                        return (CancelTransac(vmodel));

                    default:
                        ViewBag.ticket = false;
                        return View(vmodel);

                }
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
        #endregion

        #region Detail
        [HandleError]
        [HttpPost]
        public ActionResult RefreshDetails(string numTransaction, string terminalId, TrDetailsViewModel vmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ////Add detail
                    //to do --> provisoire language = 1 = French
                    //var language = "1";
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = ConfigurationManager.AppSettings["Language"];
                    }
                    string language = Session["Language"].ToString();
                    TransactionBL.AddNewTransactionDetail(vmodel.AddProduct, terminalId, numTransaction, vmodel.Minus, language);
                }
                //Find details with id transaction  + Add itemSubTotal
                var detailsListTot = TransactionBL.ListDetailsWithTot(numTransaction);
                //Sum subTotItems 
                ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                vmodel.DetailsListWithTot = detailsListTot;
                return PartialView("_PartialTransactionDetail", vmodel);
            }
            catch (InvalidOperationException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                ViewBag.ErrorAdd = "N° de produit invalide";
                return PartialView("_PartialTransactionDetail", vmodel);
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
        #endregion

        #region Search
        //POST:
        [HttpPost]
        public ActionResult SearchProduct(string product)
        {
            TrSearchViewModel vm = new TrSearchViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = ConfigurationManager.AppSettings["Language"];
                    }
                    string language = Session["Language"].ToString();
                    if (int.TryParse(product, out int codeP))
                    {
                        //vm.Products = ProductBL.FindAllProductByCode(product);
                        vm.Products = ProductBL.FindAllProductByCode(product, language);
                    }
                    else
                    {
                        vm.Products = ProductBL.FindAllProductByName(product, language);
                    }
                }
                return PartialView("_PartialTransactionSearch", vm);
            }
            catch (InvalidOperationException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                ViewBag.ErrorSearch = "N° de produit invalide";
                return PartialView("_PartialTransactionSearch", vm);
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

        [HttpGet]
        public ActionResult SearchAllProduct()
        {
            TrSearchViewModel vm = new TrSearchViewModel();
            if (Session["Language"] == null)
            {
                Session["Language"] = ConfigurationManager.AppSettings["Language"];
            }
            string language = Session["Language"].ToString();
            vm.Products = ProductBL.FindAllProduct(language);
            return PartialView("_PartialTransactionSearch", vm);
        }

        //POST:
        [HttpPost]
        public ActionResult SearchBy(string method)
        {
            try
            {
                TrSearchViewModel vm = new TrSearchViewModel();
                if (Session["Language"] == null)
                {
                    Session["Language"] = ConfigurationManager.AppSettings["Language"];
                }
                string language = Session["Language"].ToString();
                string meth = method.ToLower();
                switch (meth)
                {
                    case "brand":
                        return (SearchByBrand(vm));

                    case "hero":
                        return (SearchByHero(vm));

                    case "age":
                        return (SearchByAge(vm));

                    case "cat":
                        return (SearchByCat(vm, language));

                    default:
                        ViewBag.ticket = false;
                        return PartialView("_PartialTransactionSearch", vm);

                }
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

        private ActionResult SearchByCat(TrSearchViewModel vm, string language)
        {
            //vm.Cats = SearchBL.FindCatsParentList();
            vm.Cats = SearchBL.FindCatsParentList(language);
            return PartialView("_PartialTransactionSearch", vm);
        }

        private ActionResult SearchByAge(TrSearchViewModel vm)
        {
            vm.Ages = SearchBL.FindAgesList();
            return PartialView("_PartialTransactionSearch", vm);
        }

        private ActionResult SearchByHero(TrSearchViewModel vm)
        {
            vm.Heros = SearchBL.FindHerosList();
            return PartialView("_PartialTransactionSearch", vm);
        }

        private ActionResult SearchByBrand(TrSearchViewModel vm)
        {
            vm.Brands = SearchBL.FindBrandsList();
            return PartialView("_PartialTransactionSearch", vm);
        }

        //POST:
        [HttpPost]
        public ActionResult ProductBy(string method, string argument, TrSearchViewModel vmodel)
        {
            try
            {
                if (Session["Language"] == null)
                {
                    Session["Language"] = ConfigurationManager.AppSettings["Language"];
                }
                string language = Session["Language"].ToString();
                string meth = method.ToLower();
                switch (meth)
                {
                    case "brand":
                        return (ProductByBrand(argument, vmodel, language));

                    case "hero":
                        return (ProductByHero(argument, vmodel, language));

                    case "age":
                        return (ProductByAge(argument, vmodel, language));

                    case "cat":
                        return (ProductByCat(argument, vmodel, language));

                    default:
                        ViewBag.ticket = false;
                        return PartialView("_PartialTransactionSearch", vmodel);

                }
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

        private ActionResult ProductByBrand(string argument, TrSearchViewModel vmodel, string language)
        {
            //vmodel.Products = SearchBL.FindProductListByIdBrand(argument);
            vmodel.Products = SearchBL.FindProductListByIdBrand(argument, language);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByHero(string argument, TrSearchViewModel vmodel, string language)
        {
            //vmodel.Products = SearchBL.FindProductListByIdHero(argument);
            vmodel.Products = SearchBL.FindProductListByIdHero(argument,language);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByAge(string argument, TrSearchViewModel vmodel, string language)
        {
            //vmodel.Products = SearchBL.FindProductListByIdAge(argument);
            vmodel.Products = SearchBL.FindProductListByIdAge(argument, language);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByCat(string argument, TrSearchViewModel vmodel, string language)
        {
            //search if cat have child cat
            var children = SearchBL.FindCatsChildList(argument, language);
            if (children.Count() == 0)
            {
                //vmodel.Products = SearchBL.FindProductListByIdCat(argument);
                vmodel.Products = SearchBL.FindProductListByIdCat(argument, language);
            }
            else
            {
                vmodel.CatsChild = children;
            }
            return PartialView("_PartialTransactionSearch", vmodel);
        }
        #endregion

        #region Payment
        private ActionResult Payment(string globalDiscount, TrIndexViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //save part of transaction
                TransactionBL.SaveTransactionBeforePayment(vmodel.NumTransaction, vmodel.GlobalTot, globalDiscount);
                var gTot = vmodel.GlobalTot;
                var nTransac = vmodel.NumTransaction;
                return RedirectToAction("Index", "Pay", new { gTot, nTransac });
            }
            var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
            //Sum subTotItems 
            ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            vmodel.DetailsListWithTot = detailsListTot;
            return View(vmodel);
        }
        #endregion

        #region Cancel
        private ActionResult CancelTransac(TrIndexViewModel vmodel)
        {
            if (string.IsNullOrEmpty(vmodel.NumTransaction))
            {
                var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
                //Sum subTotItems 
                ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                vmodel.DetailsListWithTot = detailsListTot;
                return View("Index", vmodel);
            }
            TransactionBL.CancelTransac(vmodel.NumTransaction);
            return RedirectToAction("Transaction", "Home");
        }

        #endregion
    }
}