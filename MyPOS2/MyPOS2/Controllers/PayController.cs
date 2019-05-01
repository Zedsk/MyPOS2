using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPOS2.BL;
using MyPOS2.Models.Transactions;
using MyPOS2.Models.Transactions.Ticket;

namespace MyPOS2.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        #region Index
        // GET: Pay
        [HandleError]
        [HttpGet]
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        //[Authorize(Roles = "vendor")]
        public ActionResult Index(string gTot, string nTransac)
        {
            try
            {
                TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel();
                if (string.IsNullOrEmpty(nTransac))
                {
                    throw new NullReferenceException();
                }
                else
                {
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = ConfigurationManager.AppSettings["Language"];
                    }
                    string language = Session["Language"].ToString();

                    if (string.IsNullOrEmpty(gTot))
                    {
                        gTot = TransactionBL.FindTotalByTransacId(nTransac);
                    }
                    var listAmounts = PaymentBL.MakeAmountsList(nTransac);
                    if (listAmounts.Count == 0)
                    {
                        vm.GlobalTotal = gTot;
                        ViewBag.tot = gTot;
                        ViewBag.ticket = false;
                    }
                    else
                    {
                        vm.AmountsPaid = listAmounts;
                        decimal result = PaymentBL.AdaptTotalWithPaid(gTot, listAmounts);
                        if (result < 0)
                        {
                            decimal temp = Math.Abs(result);
                            vm.CashReturn = temp.ToString();
                            ViewBag.cashBack = temp.ToString();
                            vm.GlobalTotal = "0";
                            ViewBag.tot = "0";
                            //to do --> change init isChange...
                            bool isChange = false;
                            vm.Ticket = TicketBL.FillTicket(nTransac, language, isChange);
                            ViewBag.ticket = true;
                        }
                        else if (result == 0)
                        {
                            ViewBag.cashBack = "0";
                            vm.GlobalTotal = "0";
                            ViewBag.tot = "0";
                            //to do --> change init isChange...
                            bool isChange = false;
                            vm.Ticket = TicketBL.FillTicket(nTransac, language, isChange);
                            ViewBag.ticket = true;
                        }
                        else
                        {
                            vm.GlobalTotal = result.ToString();
                            ViewBag.tot = result.ToString();
                            ViewBag.ticket = false;
                        }
                    }
                    vm.NumTransaction = nTransac;
                    ViewBag.transac = nTransac;
                }
                vm.MethodsP = PaymentBL.FindMethodsList();
                vm.Languages = LanguageBL.FindLanguageListWithoutUniversal();
                ViewBag.messageCard = "";
                return View(vm);
            }
            catch (NullReferenceException ex)
            {
                //to do insert to log file
                var e1 = ex.GetBaseException(); // --> log
                var e4 = ex.Message; // --> log
                var e5 = ex.Source; // --> log
                var e8 = ex.GetType(); // --> log
                var e9 = ex.GetType().Name; // --> log

                ViewBag.Error = "Il n'y a pas de transaction en cours !";
                return View("Error");
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

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string submitButton, TrPaymentMenuViewModel vmodel)
        {
            try
            {
                switch (submitButton)
                {
                    case "Payment":
                        return (Payment(vmodel));

                    case "End":
                        return (EndTransac(vmodel));

                    case "Cancel":
                        return (CancelTransac(vmodel));

                    case "Back":
                        return (BackTransac(vmodel));

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

        #region Payment
        private ActionResult Payment(TrPaymentMenuViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                if (Session["Language"] == null)
                {
                    Session["Language"] = ConfigurationManager.AppSettings["Language"];
                }
                string language = Session["Language"].ToString();
                switch (vmodel.MethodP)
                {
                    //method cash
                    case "1":
                        return (PayCash(vmodel, language));

                    //method debit card
                    case "2":
                        //simulation
                        if (vmodel.PayCardConfirmed)
                        {
                            return (PayCardDebit(vmodel, language));
                        }
                        return (PayCardDebitNotConfirmed(vmodel));

                    //method credit card
                    case "3":
                        ////simulation same process CardDebit
                        if (vmodel.PayCardConfirmed)
                        {
                            return (PayCardDebit(vmodel, language));
                        }
                        return (PayCardDebitNotConfirmed(vmodel));

                    //method voucher
                    case "4":
                        //same process PayCash
                        return (PayCash(vmodel, language));

                    default:
                        ViewBag.tot = vmodel.GlobalTotal;
                        ViewBag.amount = vmodel.Amount;
                        ViewBag.cashBack = vmodel.CashReturn;
                        vmodel.MethodsP = PaymentBL.FindMethodsList();
                        vmodel.Languages = LanguageBL.FindLanguageListWithoutUniversal();
                        ViewBag.messageCard = "";
                        ViewBag.ticket = false;
                        return View(vmodel);
                }
            }
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            vmodel.Languages = LanguageBL.FindLanguageListWithoutUniversal();
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            ViewBag.messageCard = "";
            ViewBag.ticket = false;
            return View(vmodel);
        }


        private ActionResult EndTransac(TrPaymentMenuViewModel vmodel)
        {
            if (vmodel.GlobalTotal == "0")
            {
                //to do --> print ticket  ???
                //add n° ticket & close transaction
                TransactionBL.CloseTransac(vmodel.NumTransaction, vmodel.DateT.ToString());
                
                return RedirectToAction("Transaction", "Home");
            }
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            vmodel.Languages = LanguageBL.FindLanguageListWithoutUniversal();
            ViewBag.nopay = "La transaction n'est pas payée!";
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            ViewBag.ticket = false;
            return View(vmodel);
        }

        private ActionResult CancelTransac(TrPaymentMenuViewModel vmodel)
        {
            if (string.IsNullOrEmpty(vmodel.NumTransaction))
            {
                //to do --> ???
                return RedirectToAction("Transaction", "Home");
            }
            else
            {
                TransactionBL.CancelTransac(vmodel.NumTransaction);
                return RedirectToAction("Transaction", "Home");
            }
        }

        private ActionResult BackTransac(TrPaymentMenuViewModel vmodel)
        {
            return RedirectToAction("TransacBack", "Transaction", vmodel);
        }

        private ActionResult PayCash(TrPaymentMenuViewModel vmodel, string language)
        {
            var temp = vmodel.Amount.Replace(".", ",");
            decimal cash = decimal.Parse(temp);
            // legal limit for cash
            if (cash <= 3000)
            {
                PaymentBL.CalculCash(vmodel);
            }
            else
            {
                @ViewBag.limitCash = "Montant cash max de 3000 € dépassé !";
            }
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            vmodel.AmountsPaid = PaymentBL.MakeAmountsList(vmodel.NumTransaction);
            ViewBag.messageCard = "";
            if (ViewBag.tot == "0")
            {
                //to do --> change init isChange...
                bool isChange = false;
                vmodel.Ticket = TicketBL.FillTicket(vmodel.NumTransaction, language, isChange);
                vmodel.Language = vmodel.Ticket.Language;
                vmodel.Languages = LanguageBL.FindLanguageListWithoutUniversal();
                vmodel.DateT = vmodel.Ticket.DateTicket;
                ViewBag.DateTi = vmodel.Ticket.DateTicket;
                ViewBag.ticket = true;
            }
            else
            {
                ViewBag.ticket = false;
            }
            return View(vmodel);
        }

        private ActionResult PayCardDebit(TrPaymentMenuViewModel vmodel, string language)
        {
            PaymentBL.CalculCash(vmodel);
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            if (ViewBag.tot == "0")
            {
                //to do --> change init isChange...
                bool isChange = false;
                vmodel.Ticket = TicketBL.FillTicket(vmodel.NumTransaction, language, isChange);
                vmodel.Language = vmodel.Ticket.Language;
                vmodel.Languages = LanguageBL.FindLanguageListWithoutUniversal();
                ViewBag.DateTi = vmodel.Ticket.DateTicket;
                ViewBag.ticket = true;
            }
            else
            {
                ViewBag.ticket = false;
            }
            vmodel.AmountsPaid = PaymentBL.MakeAmountsList(vmodel.NumTransaction);
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            return View(vmodel);
        }

        private ActionResult PayCardDebitNotConfirmed(TrPaymentMenuViewModel vmodel)
        {
            vmodel.PayCardToConfirm = true;
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.ticket = false;
            return View(vmodel);
        }

        ////// same simulation process PayCardDebit
        //private ActionResult PayCardCredit(TrPaymentMenuViewModel vmodel)
        //{
        //    vmodel.Resp = TransactionBL.AskValidationCard(vmodel.Amount);
        //    if (vmodel.Resp == 1)
        //    {
        //        //to do --> create payment
        //        ViewBag.messageCard = "Demande acceptée !";
        //        ViewBag.tot = vmodel.GlobalTot;
        //        ViewBag.amount = vmodel.Amount;
        //        ViewBag.cashBack = vmodel.CashReturn;
        //        if (ViewBag.tot == "0")
        //        {
        //            vmodel.Ticket = TransactionBL.FillTicket(vmodel.NumTransaction);
        //            //vmodel.NumTicket = vmodel.Ticket.Ticket;
        //            ViewBag.NumT = vmodel.Ticket.Ticket;
        //            vmodel.NumTicket = vmodel.Ticket.Ticket;
        //            ViewBag.ticket = true;
        //        }
        //        else
        //        {

        //            ViewBag.ticket = false;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.messageCard = "Demande refusée !";
        //        ViewBag.tot = vmodel.GlobalTot;
        //        ViewBag.amount = "";
        //        ViewBag.cashBack = "0";
        //        ViewBag.ticket = false;
        //    }
        //    vmodel.MethodsP = TransactionBL.FindMethodsList();
        //    return View(vmodel);
        //}
        #endregion

        #region OptionsTicket
        public ActionResult ChangeLanguageTicket(TrPaymentMenuViewModel vmodel)
        {
            bool? isChange = true;
            vmodel.Ticket = TicketBL.FillTicket(vmodel.NumTransaction, vmodel.Language, isChange);
            ViewBag.ticket = true;
            switch(vmodel.Language)
            {
                case "1":
                    return PartialView("_PartialTicket", vmodel.Ticket);
                case "2":
                    return PartialView("_PartialTicketNl", vmodel.Ticket);
                case "3":
                    return PartialView("_PartialTicketDe", vmodel.Ticket);
                case "4":
                    return PartialView("_PartialTicketEn", vmodel.Ticket);
                default:
                    return PartialView("_PartialTicket", vmodel.Ticket);
            }
        }
        #endregion

        #region RprintTicket
        [HttpGet]
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        //[Authorize(Roles = "vendor")]
        public ActionResult RprintTicket()
        {
            try
            {
                TrRprintTicketViewModel vm = new TrRprintTicketViewModel();
                vm.MethodsP = PaymentBL.FindMethodsList();
                vm.Languages = LanguageBL.FindLanguageListWithoutUniversal();
                ViewBag.ticket = false;
                return View(vm);
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

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RprintTicket(TrRprintTicketViewModel vmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TrRprintTicketViewModel vm = new TrRprintTicketViewModel
                    {
                        Tickets = TicketBL.FindTicket(vmodel),
                        MethodsP = PaymentBL.FindMethodsList(),
                        Languages = LanguageBL.FindLanguageListWithoutUniversal()
                    };
                    ViewBag.ticket = true;
                    return View(vm);
                }
                vmodel.MethodsP = PaymentBL.FindMethodsList();
                vmodel.Languages = LanguageBL.FindLanguageListWithoutUniversal();
                ViewBag.ticket = false;
                return View(vmodel);
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

        #region ProductBack
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        //[Authorize(Roles = "vendor")]
        public ActionResult ProductBack(string nTransac)
        {
            TrProductBackViewModel vm = new TrProductBackViewModel();
            if (Session["Language"] == null)
            {
                Session["Language"] = ConfigurationManager.AppSettings["Language"];
            }
            string language = Session["Language"].ToString();
            //to do --> change init isChange..
            bool isChange = false;
            vm.Ticket = TicketBL.FillTicket(nTransac, language, isChange);
            vm.Language = language;
            vm.Languages = LanguageBL.FindLanguageList();
            vm.NumTransaction = nTransac;
            vm.DateT = vm.Ticket.DateTicket;
            ViewBag.DateTi = vm.Ticket.DateTicket;
            return View(vm);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductBack(string submitButton, TrProductBackViewModel vmodel)
        {
            try
            {
                TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel
                {
                    NumTransaction = vmodel.NumTransaction,
                    DateT = vmodel.DateT,
                    GlobalTotal = "0"
                };
                switch (submitButton.ToLower())
                {
                    case "end":
                        return (EndTransac(vm));

                    case "cancel":
                        return (CancelTransac(vm));

                    default:
                        vmodel.Languages = LanguageBL.FindLanguageList();
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

    }
}