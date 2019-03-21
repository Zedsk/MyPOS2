using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using MyPOS2.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class PaymentBL
    {
        internal static TrPaymentMenuViewModel CalculCash(TrPaymentMenuViewModel vmodel)
        {
            var temp1 = vmodel.GlobalTotal.Replace(".", ",");
            var temp2 = vmodel.Amount.Replace(".", ",");
            decimal cash = decimal.Parse(temp2);
            decimal tot = decimal.Parse(temp1);
            int meth = int.Parse(vmodel.MethodP);
            int transac = int.Parse(vmodel.NumTransaction);

            using (IDalPayment dal = new DalPayment())
            {
                if (cash > tot)
                {
                    dal.CreatePayment(tot, meth, transac);
                    vmodel.CashReturn = (cash - tot).ToString();
                    vmodel.Amount = "0";
                    vmodel.GlobalTotal = "0";
                }
                else if (cash == tot)
                {
                    dal.CreatePayment(tot, meth, transac);
                    vmodel.CashReturn = "0";
                    vmodel.Amount = "0";
                    vmodel.GlobalTotal = "0";
                }
                else if (cash < tot)
                {
                    dal.CreatePayment(cash, meth, transac);
                    vmodel.CashReturn = "0";
                    vmodel.Amount = "0";
                    vmodel.GlobalTotal = (tot - cash).ToString();
                }
                return vmodel;
            }
        }


        //internal static TrPaymentMenuViewModel CalculCash(string total, string cashIn, string method, string numtransac)
        //{
        //    var temp1 = total.Replace(".", ",");
        //    var temp2 = cashIn.Replace(".", ",");
        //    decimal cash = decimal.Parse(temp2);
        //    decimal tot = decimal.Parse(temp1);
        //    int meth = int.Parse(method);
        //    int transac = int.Parse(numtransac);
        //    TrPaymentMenuViewModel vm = new TrPaymentMenuViewModel();
        //    using (IDalTransaction dal = new DalTransaction())
        //    {
        //        if (cash > tot)
        //        {
        //            //dal.CreatePayment(tot, meth, transac);
        //            vm.CashReturn = (cash - tot).ToString();
        //            vm.Amount = "0";
        //            vm.GlobalTot = "0";
        //        }
        //        else if (cash == tot)
        //        {
        //            //dal.CreatePayment(tot, meth, transac);
        //            vm.CashReturn = "0";
        //            vm.Amount = "0";
        //            vm.GlobalTot = "0";
        //        }
        //        else if (cash < tot)
        //        {
        //            //dal.CreatePayment(cash, meth, transac);
        //            vm.CashReturn = "0";
        //            vm.Amount = "0";
        //            vm.GlobalTot = (tot - cash).ToString();
        //        }
        //        return vm;
        //    }
        //}

        internal static int AskValidationCard(string amount)
        {
            Random random = new Random();
            int val = random.Next(2);
            return val;
        }

        internal static IList<PAYMENT_METHOD> FindMethodsList()
        {
            using (IDalPayment dal = new DalPayment())
            {
                return dal.GetAllMethods();
            }
        }

        internal static List<string> MakeAmountsList(string amount, List<string> amountsList)
        {
            if (amountsList == null)
            {
                List<string> newList = new List<string>
                {
                    amount
                };
                return newList;
            }
            amountsList.Add(amount);
            return amountsList;
        }

        internal static List<string> MakeAmountsList(string numTransaction)
        {
            IList<PAYMENT> listPayments = new List<PAYMENT>();
            List<string> amountsList = new List<string>();
            listPayments = FindPaymentsByTransacId(numTransaction);
            foreach (var payment in listPayments)
            {
                amountsList.Add((payment.amount).ToString());
            }
            return amountsList;
        }

        internal static decimal AdaptTotalWithPaid(string gTot, List<string> listAmounts)
        {
            decimal amounts = 0;
            for (int i = 0; i < listAmounts.Count; i++)
            {
                amounts += decimal.Parse(listAmounts[i]);
            }
            var temp = gTot.Count();
            string tot = gTot.Replace(".", ",");
            decimal result = decimal.Parse(tot) - amounts;
            return result;
        }

        internal static IList<PAYMENT> FindPaymentsByTransacId(string numTransaction)
        {
            using (IDalPayment dal = new DalPayment())
            {

                int tr = int.Parse(numTransaction);
                return dal.GetAllPaymentsByTransacId(tr);
            }
        }
    }
}