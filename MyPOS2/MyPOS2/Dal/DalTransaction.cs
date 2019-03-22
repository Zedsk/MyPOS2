﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    public class DalTransaction : IDalTransaction
    {
        private Pos1Entities db;

        public DalTransaction()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        #region Transaction
        public int CreateTransaction(int terminal, string vendorId, int shop, int customer)
        {
            STATUS status = db.STATUSs.Where(s => s.nameStatus.ToLower() == "open").Single();
            // to do --> provisoire messageId = 1 languageId = 1
            TRANSACTIONS tr = new TRANSACTIONS { transactionDateBegin = DateTime.Now, transactionDateEnd = DateTime.Parse("2000-01-01 00:00:00"), terminalId = terminal, userId = vendorId, shopId = shop, statusId = status.idStatus, customerId = customer, messageId = 1, languageId = 1 };
            db.TRANSACTIONSs.Add(tr);
            db.SaveChanges();
            return tr.idTransaction;
        }

        public void CreateDetail(PRODUCT prod, int termId, int trId, decimal vat)
        {
            //// name item -> barcode  à changer!
            db.TRANSACTION_DETAILSs.Add(new TRANSACTION_DETAILS { transactionId = trId, productId = prod.idProduct, nameItem = prod.barcode, price = prod.salesPrice, quantity = 1, Discount = prod.discountRate, vatItem = vat});
            db.SaveChanges();
        }

        public void CreateDetail(PRODUCT prod, int termId, int trId, decimal vat, string nameProd)
        {
            db.TRANSACTION_DETAILSs.Add(new TRANSACTION_DETAILS { transactionId = trId, productId = prod.idProduct, nameItem = nameProd, price = prod.salesPrice, quantity = 1, Discount = prod.discountRate, vatItem = vat });
            db.SaveChanges();
        }

        public List<TRANSACTION_DETAILS> GetAllDetailsByTransactionId(int id)
        {
            return db.TRANSACTION_DETAILSs.Where(t => t.transactionId == id).ToList();
        }

        public TRANSACTIONS GetTransactionById(int transactionId)
        {
            return db.TRANSACTIONSs.Where(t => t.idTransaction == transactionId).Single();
        }

        public void EditQtyToDetailById(int id, int qty)
        {
            var detail = db.TRANSACTION_DETAILSs.First(d => d.idTransactionDetails == id);
            if(detail != null)
            {
                detail.quantity = qty;
                db.SaveChanges();
            }
        }

        public void DeleteDetail(int id)
        {
            TRANSACTION_DETAILS detail = db.TRANSACTION_DETAILSs.Find(id);
            db.TRANSACTION_DETAILSs.Remove(detail);
            db.SaveChanges();
        }

        public void UpdateTransaction(int transactionId, decimal globalTotal, decimal? discountG)
        {
            var transac = db.TRANSACTIONSs.First(d => d.idTransaction == transactionId);
            if (transac != null)
            {
                transac.total = globalTotal;
                transac.discountGlobal = discountG;
                //transac.vatId = globalVAT;

                db.SaveChanges();
            }
        }

        public void UpdateTransactionMessageId(int transactionId, int idMessage)
        {

            var transac = db.TRANSACTIONSs.First(t => t.idTransaction == transactionId);
            if (transac != null)
            {
                transac.messageId = idMessage;
                db.SaveChanges();
            }
        }

        public void CancelTransactionById(int transactionId)
        {
            var transac = db.TRANSACTIONSs.First(d => d.idTransaction == transactionId);
            if (transac != null)
            {
                // canceled = 3
                transac.statusId = 3;
                transac.transactionDateEnd = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void CloseTransaction(int transacId)
        {
            var transac = db.TRANSACTIONSs.First(d => d.idTransaction == transacId);
            if (transac != null)
            {
                //transac.messageId = message;
                //close = 2
                transac.statusId = 2;
                transac.transactionDateEnd = DateTime.Now;
                db.SaveChanges();
            }
        }

        #endregion
    }
}