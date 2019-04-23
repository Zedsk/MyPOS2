﻿using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using MyPOS2.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class TransactionBL
    {
        internal static string InitializeNewTransaction(int terminal, string currentId)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                // to do --> provisoire currentId value for test.
                if (string.IsNullOrEmpty(currentId))
                {
                    currentId = "8a4759b1-391e-48d6-baa3-ff95c7640915";
                }
                // to do --> provisoire shopId = 1, customerId = 1
                int shop = 1;
                int customer = 1;

                int t = dal.CreateTransaction(terminal, currentId, shop, customer);
                return t.ToString();
            }
        }

        internal static IList<TRANSACTION_DETAILS> InitializeTransactionDetails()
        {
            IList<TRANSACTION_DETAILS> result = new List<TRANSACTION_DETAILS>();
            TRANSACTION_DETAILS detail = new TRANSACTION_DETAILS();
            result.Add(detail);
            return result;

        }

        internal static void AddNewTransactionDetail(string codeProduct, string terminal, string transaction, bool minus, string language)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                //find productId with codeProduct
                PRODUCT prod;
                using (IDalProduct dalP = new DalProduct())
                {
                    prod = dalP.GetProductByCode(codeProduct);
                }
                //verify if product exist in detail and Add or Remove itemDetail
                IList<TRANSACTION_DETAILS> detailList = FindTransactionDetailsListById(transaction);
                var result = VerifyProductInDetail(prod.idProduct, detailList);
                if (result)
                {
                    //Product exist --> Modify qty
                    foreach (var item in detailList)
                    {
                        if (item.productId == prod.idProduct)
                        {
                            var newqty = 0;
                            if (minus)
                            {
                                //qty--
                                newqty = item.quantity - 1;
                                if (newqty == 0)
                                {
                                    dal.DeleteDetail(item.idTransactionDetails);
                                    break;
                                }
                                dal.EditQtyToDetailById(item.idTransactionDetails, newqty);
                                break;
                            }
                            else
                            {
                                //qty++
                                newqty = item.quantity + 1;
                                dal.EditQtyToDetailById(item.idTransactionDetails, newqty);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    ////Add new detail --> param product, terminalId, transactionId, vatItem 
                    decimal vatItem;
                    using (IDalVat dalV = new DalVat())
                    {
                        vatItem = dalV.GetVatValById(prod.vatId);
                    }
                    int terminalId = int.Parse(terminal);
                    int transactionId = int.Parse(transaction);
                    int languageId = LanguageBL.FindIdLanguageByShortForm(language);
                    string name;
                    using (IDalProduct dalP = new DalProduct())
                    {
                        name = dalP.GetNameProductById(prod.idProduct, languageId);
                    }
                    dal.CreateDetail(prod, terminalId, transactionId, vatItem, name);
                }
            }
        }

        public static bool VerifyProductInDetail(int idProd, IList<TRANSACTION_DETAILS> detailList)
        {
            var result = false;
            if (detailList.Count != 0)
            {
                foreach (var item in detailList)
                {
                    if (item.productId == idProd)
                    {
                        result = true;
                    }
                }
                return result;
            }
            else
            {
                return result;
            }
        }

        internal static IList<TRANSACTION_DETAILS> FindTransactionDetailsListById(string transaction)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                int transactionId = int.Parse(transaction);
                return dal.GetAllDetailsByTransactionId(transactionId);
            }

        }

        internal static TRANSACTIONS FindTransactionById(string numTransaction)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                int transactionId = int.Parse(numTransaction);
                return dal.GetTransactionById(transactionId);
            }
        }

        internal static IList<TrDetailsViewModel> AddSubTotalPerDetailToList(IList<TRANSACTION_DETAILS> detailsList)
        {
            IList<TrDetailsViewModel> vmList = new List<TrDetailsViewModel>();
            foreach (var item in detailsList)
            {
                var p = item.price;
                var q = item.quantity;
                var d = item.discount;
                decimal? st;
                if (p == 0 || q == 0)
                {
                    st = 0;
                }
                else if (d == 0 || d == null)
                {
                    st = (p * q);
                }
                else
                {
                    //ex: (100*2)*0.05 = 10
                    var temp = (p * q) * d;
                    st = (p * q) - temp;
                }

                //find code product
                int id = item.productId;
                string code = ProductBL.FindCodeProductById(id);
                TrDetailsViewModel vm = new TrDetailsViewModel
                {
                    ProductName = item.nameItem,
                    ProductCode = code,
                    Price = p,
                    Quantity = q,
                    ProductVat = item.vatItem,
                    Discount = item.discount,
                    TotalItem = st
                };
                vmList.Add(vm);
            }

            return vmList;
        }
                
        internal static decimal? SumItemsSubTot(IList<TrDetailsViewModel> detailsListTot)
        {
            decimal? result = 0;
            foreach (var vm in detailsListTot)
            {
                result += vm.TotalItem;
            }
            return result;
        }

        internal static IList<TrDetailsViewModel> ListDetailsWithTot(string numTransaction)
        {
            var detailsList = FindTransactionDetailsListById(numTransaction);
            var detailsListTot = TransactionBL.AddSubTotalPerDetailToList(detailsList);
            return detailsListTot;

        }

        internal static void SaveTransactionBeforePayment(string numTransaction, string globalTotal, string discountG)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                var idTr = int.Parse(numTransaction);
                //var gTot = decimal.Parse(globalTotal);
                //probleme le string "157.98" devient 15798
                var temp = globalTotal.Replace(".", ",");
                var gTot = decimal.Parse(temp);

                decimal? gDisc = null;
                if (discountG != "")
                {
                    gDisc = decimal.Parse(discountG);
                    gDisc /= 100;
                }
                dal.UpdateTransaction(idTr, gTot, gDisc);
            }
        }

        internal static void CancelTransac(string numTransaction)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                var transac = int.Parse(numTransaction);
                dal.CancelTransactionById(transac);
            }
        }

        internal static void CloseTransac(string numTransaction)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                var transac = int.Parse(numTransaction);
                dal.CloseTransaction(transac);
            }
        }

        internal static void CloseTransac(string numTransaction, string dateTicket)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                int transac = int.Parse(numTransaction);
                DateTime date = DateTime.Parse(dateTicket);
                dal.CloseTransaction(transac, date);
            }
        }

        internal static string FindTotalByTransacId(string nTransac)
        {
            TRANSACTIONS transac = FindTransactionById(nTransac);
            return transac.total.ToString();
        }

        internal static IList<TRANSACTIONS> FindTransactionsDay()
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                DateTime date = DateTime.Today;
                IList<TRANSACTIONS> result = new List<TRANSACTIONS>();
                IList<SPP_TransactionsDay_Result> tDay = dal.GetAllTransactionDay(date);
                for (int i = 0; i < tDay.Count(); i++)
                {
                    TRANSACTIONS transac = new TRANSACTIONS
                    {
                        idTransaction = tDay[i].idTransaction,
                        transactionDateBegin = tDay[i].transactionDateBegin,
                        transactionDateEnd = tDay[i].transactionDateEnd,
                        total = tDay[i].total,
                        discountGlobal = tDay[i].discountGlobal,
                        isReturn = tDay[i].isReturn,
                        terminalId = tDay[i].terminalId,
                        shopId = tDay[i].shopId,
                        customerId = tDay[i].customerId,
                        statusId = tDay[i].statusId,
                        userId = tDay[i].userId
                    };
                    result.Add(transac);
                }
                return result;
            }
        }

        internal static void SaveTransactionProductBack(string numTransaction, string globalTotal)
        {
            using (IDalTransaction dal = new DalTransaction())
            {
                var idTr = int.Parse(numTransaction);
                //var gTot = decimal.Parse(globalTotal);
                //probleme le string "157.98" devient 15798
                var temp = globalTotal.Replace(".", ",");
                var gTot = decimal.Parse(temp);
                bool isReturn = true;
                dal.UpdateTransaction(idTr, gTot, isReturn);
            }
        }
    }
}