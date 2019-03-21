using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPOS2.BL;
using MyPOS2.Models.Transactions;

namespace MyPOS2.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        [HandleError]
        [HttpGet]
        //public ActionResult Index(TrPaymentMenuViewModel vm)
        public ActionResult Index(string gTot, string nTransac)
        {
            try
            {
                TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel();
                //if (gTot == null || nTransac == null)
                if (string.IsNullOrEmpty(nTransac))
                {
                    //provisoire
                    vm.GlobalTotal = "399.97";
                    vm.NumTransaction = "11";
                    ViewBag.tot = "399.97";
                    ViewBag.transac = "11";
                    ViewBag.ticket = false;
                    //ViewBag.amount = "0";
                    //ViewBag.cashBack = "0";
                    //throw new NullReferenceException();
                }
                else
                {
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
                            vm.Ticket = TicketBL.FillTicket(nTransac);
                            ViewBag.ticket = true;
                        }
                        else if (result == 0)
                        {
                            ViewBag.cashBack = "0";
                            vm.GlobalTotal = "0";
                            ViewBag.tot = "0";
                            vm.Ticket = TicketBL.FillTicket(nTransac);
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
                    //ViewBag.amount = "0";
                    //ViewBag.cashBack = "0";
                }
                vm.MethodsP = PaymentBL.FindMethodsList();
                //vm.PayCardToConfirm = false;
                ViewBag.messageCard = "";
                ////provisoire
                //vm.GlobalTot = "123.99";
                //vm.NumTransaction = "999";
                ////ViewBag.Amount = vm.GlobalTot;
                return View(vm);
            }
            catch (NullReferenceException ex)
            {
                //TempData["Error"] = "Il n'y a pas de transaction en cours !";
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

        private ActionResult Payment(TrPaymentMenuViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //if (vmodel.GlobalTot == "0")
                //{
                //    ViewBag.nopay = "La transaction est payée!";
                //    ViewBag.tot = vmodel.GlobalTot;
                //    //ViewBag.transac = vmodel.NumTransaction;
                //    ViewBag.amount = vmodel.Amount;
                //    ViewBag.cashBack = vmodel.CashReturn;
                //    vmodel.MethodsP = TransactionBL.FindMethodsList();
                //    ViewBag.ticket = true;
                //    return View(vmodel);
                //}
                TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel();
                switch (vmodel.MethodP)
                {
                    //method cash
                    case "1":
                        return (PayCash(vmodel));

                    //method debit card
                    case "2":
                        //simulation
                        if (vmodel.PayCardConfirmed)
                        {
                            return (PayCardDebit(vmodel));
                        }
                        return (PayCardDebitNotConfirmed(vmodel));

                    //method credit card
                    case "3":
                        ////simulation same process CardDebit
                        //return (PayCardCredit(vmodel));
                        if (vmodel.PayCardConfirmed)
                        {
                            return (PayCardDebit(vmodel));
                        }
                        return (PayCardDebitNotConfirmed(vmodel));

                    //method voucher
                    case "4":
                        //same process PayCash
                        return (PayCash(vmodel));

                    default:
                        ViewBag.tot = vmodel.GlobalTotal;
                        ViewBag.amount = vmodel.Amount;
                        ViewBag.cashBack = vmodel.CashReturn;
                        vmodel.MethodsP = PaymentBL.FindMethodsList();
                        ViewBag.messageCard = "";
                        ViewBag.ticket = false;
                        return View(vmodel);
                }
            }
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            //vmodel.MethodsP = TransactionBL.FindMethodsList();
            ViewBag.messageCard = "";
            ViewBag.ticket = false;
            return View(vmodel);
        }


        private ActionResult EndTransac(TrPaymentMenuViewModel vmodel)
        {
            if (vmodel.GlobalTotal == "0")
            {
                //to do --> print ticket  ???

                //to do --> add n° ticket & close transaction
                //TransactionBL.AddTicketAndCloseTransac(vmodel.NumTransaction, vmodel.NumTicket);
                TransactionBL.AddTicketAndCloseTransac(vmodel.NumTransaction);
                return RedirectToAction("Transaction", "Home");
            }
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            ViewBag.nopay = "La transaction n'est pas payée!";
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            ViewBag.ticket = false;
            return View(vmodel);
        }

        private ActionResult CancelTransac(TrPaymentMenuViewModel vmodel)
        {
            //if (vmodel.NumTransaction == "" || vmodel.NumTransaction == null)
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
            return RedirectToAction("TransacReturn", "Transaction", vmodel);
        }

        private ActionResult PayCash(TrPaymentMenuViewModel vmodel)
        {
            //vmodel.AmountsPaid = TransactionBL.MakeAmountsList(vmodel.Amount, vmodel.AmountsPaid);
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
                vmodel.Ticket = TicketBL.FillTicket(vmodel.NumTransaction);
                //vmodel.NumTicket = vmodel.Ticket.Ticket;
                ViewBag.NumT = vmodel.Ticket.Ticket;
                vmodel.NumTicket = vmodel.Ticket.Ticket;
                ViewBag.ticket = true;
            }
            else
            {
                ViewBag.ticket = false;
            }
            return View(vmodel);
        }

        private ActionResult PayCardDebit(TrPaymentMenuViewModel vmodel)
        {
            //vmodel.Resp = TransactionBL.AskValidationCard(vmodel.Amount);
            //if (vmodel.Resp == 1)
            //{
            //vmodel.AmountsPaid = PaymentBL.MakeAmountsList(vmodel.Amount, vmodel.AmountsPaid);

            PaymentBL.CalculCash(vmodel);
            //ViewBag.messageCard = "Demande acceptée !";
            ViewBag.tot = vmodel.GlobalTotal;
            ViewBag.amount = vmodel.Amount;
            ViewBag.cashBack = vmodel.CashReturn;
            //vmodel.Amount = "0";
            //vmodel.GlobalTot = "0";
            if (ViewBag.tot == "0")
            {
                vmodel.Ticket = TicketBL.FillTicket(vmodel.NumTransaction);
                //vmodel.NumTicket = vmodel.Ticket.Ticket;
                //ViewBag.NumT = vmodel.Ticket.Ticket;
                //vmodel.NumTicket = vmodel.Ticket.Ticket;
                ViewBag.ticket = true;
            }
            else
            {
                ViewBag.ticket = false;
            }
            //}
            //else
            //{
            //    ViewBag.messageCard = "Demande refusée !";
            //    ViewBag.tot = vmodel.GlobalTot;
            //    ViewBag.amount = "";
            //    ViewBag.cashBack = "0";
            //    //vmodel.Amount = "0";
            //    ViewBag.ticket = false;
            //}
            vmodel.AmountsPaid = PaymentBL.MakeAmountsList(vmodel.NumTransaction);
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            return View(vmodel);
        }

        private ActionResult PayCardDebitNotConfirmed(TrPaymentMenuViewModel vmodel)
        {
            vmodel.PayCardToConfirm = true;
            vmodel.MethodsP = PaymentBL.FindMethodsList();
            //vmodel.AmountsPaid = PaymentBL.MakeAmountsList(vmodel.NumTransaction);
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
    }
}