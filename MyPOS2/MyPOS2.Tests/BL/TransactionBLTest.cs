using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPOS2.Data.Entity;
using MyPOS2.BL;
using MyPOS2.Models.Transactions;
using System.Web.Util;

namespace MyPOS2.Tests.BL
{
    [TestClass]
    public class TransactionBLTest
    {
        [TestMethod]
        public void SumItemsSubTot_4detailsListTot25_Return100()
        {
            TrDetailsViewModel detail1 = new TrDetailsViewModel();
            TrDetailsViewModel detail2 = new TrDetailsViewModel();
            TrDetailsViewModel detail3 = new TrDetailsViewModel();
            TrDetailsViewModel detail4 = new TrDetailsViewModel();

            detail1.TotalItem = 25;
            detail2.TotalItem = 25;
            detail3.TotalItem = 25;
            detail4.TotalItem = 25;

            IList<TrDetailsViewModel> detailsListTot = new List<TrDetailsViewModel>
            {
                detail1,
                detail2,
                detail3,
                detail4
            };

            decimal result = (decimal)TransactionBL.SumItemsSubTot(detailsListTot);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void VerifyProductInDetail_ProductInDetail_ReturnTrue()
        {
            TRANSACTION_DETAILS detail1 = new TRANSACTION_DETAILS();
            TRANSACTION_DETAILS detail2 = new TRANSACTION_DETAILS();
            TRANSACTION_DETAILS detail3 = new TRANSACTION_DETAILS();
            TRANSACTION_DETAILS detail4 = new TRANSACTION_DETAILS();

            detail1.productId = 1;
            detail2.productId = 2;
            detail3.productId = 3;
            detail4.productId = 4;

            int idProd = 3;

            IList<TRANSACTION_DETAILS> detailsList = new List<TRANSACTION_DETAILS>
            {
                detail1,
                detail2,
                detail3,
                detail4
            };

            bool result = TransactionBL.VerifyProductInDetail(idProd, detailsList);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void VerifyProductInDetail_NotProductInDetail_ReturnFalse()
        {
            TRANSACTION_DETAILS detail1 = new TRANSACTION_DETAILS();
            TRANSACTION_DETAILS detail2 = new TRANSACTION_DETAILS();
            TRANSACTION_DETAILS detail3 = new TRANSACTION_DETAILS();
            TRANSACTION_DETAILS detail4 = new TRANSACTION_DETAILS();

            detail1.productId = 1;
            detail2.productId = 2;
            detail3.productId = 3;
            detail4.productId = 4;

            int idProd = 7;

            IList<TRANSACTION_DETAILS> detailsList = new List<TRANSACTION_DETAILS>
            {
                detail1,
                detail2,
                detail3,
                detail4
            };

            bool result = TransactionBL.VerifyProductInDetail(idProd, detailsList);
            Assert.AreEqual(false, result);
        }
    }
}
