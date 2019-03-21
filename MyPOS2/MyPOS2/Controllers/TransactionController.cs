using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core;
using Microsoft.AspNet.Identity;
using MyPOS2.BL;
using MyPOS2.Models.Transactions;


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
                // tot do --> modifier si plusieur cashday diff terminaux
                int terminal = TerminalBL.FindTerminalIdByDate();
                string currentId = User.Identity.GetUserId();
                TrIndexViewModel vm = new TrIndexViewModel
                {
                    ////terminal name or id
                    //vm.TerminalsNames = TransactionBL.FindTerminalsNames(); // list names terminals
                    //vm.TerminalsList = TransactionBL.FindTerminalsList();  // list terminals
                    TerminalId = terminal,

                    ////transaction num = id
                    //vm.NumTransaction = "9999";
                    // To do -> vérification si transaction en cours
                    // to do --> provisoire vendorId = 1, shopId = 1, customerId = 1
                    NumTransaction = TransactionBL.InitializeNewTransaction(terminal, currentId),

                    // to do --> quid date et heure?
                    DateDay = DateTime.Now.Date.ToString("d"),
                    HourDay = DateTime.Now.ToString("T"),

                    /*Vendor = "Toto",*/ // --> id = 1

                    //detail vide ->  provisoire
                    //vm.TransactionDetailsListById = TransactionBL.InitializeTransactionDetails();
                    //VatsList = TransactionBL.FindVatsList()
                };
                //ViewBag.subTot1 = 0;
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

        //[HttpGet]
        //public ActionResult IndexContinue()
        //{
        //    try
        //    {
        //        int terminal = TransactionBL.FindTerminalIdByDate();
        //        TrIndexViewModel vm = new TrIndexViewModel
        //        {
        //            ////terminal name or id
        //            //vm.TerminalsNames = TransactionBL.FindTerminalsNames(); // list names terminals
        //            //vm.TerminalsList = TransactionBL.FindTerminalsList();  // list terminals
        //            TerminalId = terminal,

        //            ////transaction num = id
        //            //vm.NumTransaction = "9999";
        //            // To do -> vérification si transaction en cours
        //            // to do --> provisoire vendorId = 1, shopId = 1, customerId = 1
        //            NumTransaction = TransactionBL.InitializeNewTransaction(terminal),

        //            // to do --> quid date et heure?
        //            DateDay = DateTime.Now.Date.ToString("d"),
        //            HourDay = DateTime.Now.ToString("T"),

        //            Vendor = "Toto", // --> id = 1

        //            //detail vide ->  provisoire
        //            //vm.TransactionDetailsListById = TransactionBL.InitializeTransactionDetails();
        //            VatsList = TransactionBL.FindVatsList()
        //        };
        //        //ViewBag.subTot1 = 0;
        //        return View("Index", vm);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Il y a un eu une erreur : " + ex.ToString());
        //        throw;
        //    }

        //}

        [HandleError]
        public ActionResult TransacReturn(TrPaymentMenuViewModel vmodel)
        {
            try
            {
                //to do --> check if detailsListTot count = 0 
                var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
                //if (detailsListTot.Count == 0)
                //{
                //    throw "";
                //}
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
                    //VatsList = TransactionBL.FindVatsList(),
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

        ////POST:
        //[HttpPost]
        ////public ActionResult PaymentTransaction(string numTransaction, string terminalId, string vendor, string subTotitem, string discountG, string subTot, string vat, string globalTotal)
        ////public ActionResult PaymentTransaction(string subTotitem, string discountG, string subTot, string vat, TrIndexViewModel vmodel)
        //public ActionResult Index(string globalDiscount, TrIndexViewModel vmodel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //save part of transaction
        //        //var discountG = vm.GlobalDiscount.ToString();
        //        TransactionBL.SaveTransactionBeforePayment(vmodel.NumTransaction, vmodel.GlobalTot, globalDiscount, vmodel.GlobalVAT);

        //        //TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel
        //        //{
        //        //    GlobalTot = vmodel.GlobalTot,
        //        //    NumTransaction = vmodel.NumTransaction
        //        //};
        //        //return View("PaymentMenu", vm);

        //        //return RedirectToAction("Index", "Pay", new TrPaymentMenuViewModel { GlobalTot = vmodel.GlobalTot, NumTransaction = vmodel.NumTransaction });
        //        var gt = vmodel.GlobalTot;
        //        var nt = vmodel.NumTransaction;
        //        return RedirectToAction("Index", "Pay", new { gt, nt });
        //    }
        //    var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
        //    //Sum subTotItems 
        //    ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
        //    vmodel.DetailsListWithTot = detailsListTot;
        //    vmodel.VatsList = TransactionBL.FindVatsList();
        //    return View(vmodel);
        //}

        //POST:
        [HandleError]
        [HttpPost]
        //public ActionResult PaymentTransaction(string numTransaction, string terminalId, string vendor, string subTotitem, string discountG, string subTot, string vat, string globalTotal)
        //public ActionResult PaymentTransaction(string subTotitem, string discountG, string subTot, string vat, TrIndexViewModel vmodel)
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
        //public ActionResult RefreshDetails(string addProduct, string numTransaction, string terminalId, bool minus, TrIndexViewModel vmodel)
        public ActionResult RefreshDetails(string numTransaction, string terminalId, TrDetailsViewModel vmodel)
        {
            //TrDetailsViewModel vm = new TrDetailsViewModel();
            ////Add detail
            //TransactionBL.AddNewTransactionDetail(addProduct, terminalId, numTransaction, minus);
            ////Find details with id transaction  + Add itemSubTotal
            //var detailsList = TransactionBL.FindTransactionDetailsListById(numTransaction);
            //var detailsListTot = TransactionBL.AddSubTotalPerDetailToList(detailsList);
            ////Sum subTotItems 
            //ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            //vm.DetailsListWithTot = detailsListTot;
            ////vm.DetailsListById = detailsList;

            //return PartialView("_PartialTransactionDetail", vm);
            try
            {
                if (ModelState.IsValid)
                {
                    //TrDetailsViewModel vm = new TrDetailsViewModel();
                    ////Add detail
                    TransactionBL.AddNewTransactionDetail(vmodel.AddProduct, terminalId, numTransaction, vmodel.Minus);
                    ////Find details with id transaction  + Add itemSubTotal
                    //var detailsListTot = TransactionBL.ListDetailsWithTot(numTransaction);
                    ////Sum subTotItems 
                    //ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
                    //vm.DetailsListWithTot = detailsListTot;
                    //vm.DetailsListById = detailsList;

                    //return PartialView("_PartialTransactionDetail", vm);
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
                    if (int.TryParse(product, out int codeP))
                    {
                        //var item = TransactionBL.FindProductByCode(product);
                        //vm.Product = item.barcode;
                        //vm.Image = item.imageProduct;
                        //vm.Price = (item.salesPrice).ToString();
                        vm.Products = ProductBL.FindAllProductByCode(product);
                    }
                    else
                    {
                        //var item = TransactionBL.FindProductByName(product);
                        //vm.Product = item.barcode;
                        //vm.Image = item.imageProduct;
                        //vm.Price = (item.salesPrice).ToString();

                        //to do
                        //vm.Products = TransactionBL.FindAllProductByName(product);
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

        //POST:
        [HttpPost]
        public ActionResult SearchBy(string method)
        {
            try
            {
                TrSearchViewModel vm = new TrSearchViewModel();
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
                        return (SearchByCat(vm));

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

        private ActionResult SearchByCat(TrSearchViewModel vm)
        {
            vm.Cats = SearchBL.FindCatsList();
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
                string meth = method.ToLower();
                switch (meth)
                {
                    case "brand":
                        return (ProductByBrand(argument, vmodel));

                    case "hero":
                        return (ProductByHero(argument, vmodel));

                    case "age":
                        return (ProductByAge(argument, vmodel));

                    case "cat":
                        return (ProductByCat(argument, vmodel));

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

        private ActionResult ProductByBrand(string argument, TrSearchViewModel vmodel)
        {
            vmodel.Products = SearchBL.FindProductListByIdBrand(argument);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByHero(string argument, TrSearchViewModel vmodel)
        {
            vmodel.Products = SearchBL.FindProductListByIdHero(argument);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByAge(string argument, TrSearchViewModel vmodel)
        {
            vmodel.Products = SearchBL.FindProductListByIdAge(argument);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        private ActionResult ProductByCat(string argument, TrSearchViewModel vmodel)
        {
            vmodel.Products = SearchBL.FindProductListByIdCat(argument);
            return PartialView("_PartialTransactionSearch", vmodel);
        }

        #endregion

        #region Payment
        private ActionResult Payment(string globalDiscount, TrIndexViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //save part of transaction
                //var discountG = vm.GlobalDiscount.ToString();
                TransactionBL.SaveTransactionBeforePayment(vmodel.NumTransaction, vmodel.GlobalTot, globalDiscount);

                //TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel
                //{
                //    GlobalTot = vmodel.GlobalTot,
                //    NumTransaction = vmodel.NumTransaction
                //};
                //return View("PaymentMenu", vm);

                //return RedirectToAction("Index", "Pay", new TrPaymentMenuViewModel { GlobalTot = vmodel.GlobalTot, NumTransaction = vmodel.NumTransaction });
                var gTot = vmodel.GlobalTot;
                var nTransac = vmodel.NumTransaction;
                return RedirectToAction("Index", "Pay", new { gTot, nTransac });
            }
            var detailsListTot = TransactionBL.ListDetailsWithTot(vmodel.NumTransaction);
            //Sum subTotItems 
            ViewBag.subTot1 = TransactionBL.SumItemsSubTot(detailsListTot);
            vmodel.DetailsListWithTot = detailsListTot;
            //vmodel.VatsList = TransactionBL.FindVatsList();
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
                //vmodel.VatsList = TransactionBL.FindVatsList();
                return View("Index", vmodel);
            }
            TransactionBL.CancelTransac(vmodel.NumTransaction);
            return RedirectToAction("Transaction", "Home");

        }
        #endregion
    }
}