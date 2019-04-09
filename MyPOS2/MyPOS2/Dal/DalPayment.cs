using System;
using System.Collections.Generic;
using System.Linq;
using MyPOS2.Data.Entity;


namespace MyPOS2.Dal
{
    public class DalPayment : IDalPayment
    {
        #region DB

        private Pos1Entities db;

        public DalPayment()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public void CreatePayment(decimal tot, int methodP, int numTransaction)
        {
            PAYMENT p = new PAYMENT { amount = tot, momentPay = DateTime.Now, paymentMethodId = methodP, transactionId = numTransaction };
            db.PAYMENTs.Add(p);
            db.SaveChanges();
        }

        public IList<PAYMENT_METHOD> GetAllMethods()
        {
            return db.PAYMENT_METHODs.ToList();
        }

        public IList<PAYMENT> GetAllPaymentsByTransacId(int numTransaction)
        {
            ////ne fonctionne pas ici, dans le controller ça focntionne. Modification dans TicketBL de FillTicket()
            //var test = db.PAYMENTs.Include(p => p.PAYMENT_METHOD).Where(m => m.transactionId == numTransaction).ToList();
            return db.PAYMENTs.Where(p => p.transactionId == numTransaction).ToList();
        }

        public string GetMethodNameById(int paymentMethodId)
        {
            return db.PAYMENT_METHODs.Where(p => p.idPaymentMethod == paymentMethodId).Select(m => m.method).Single();
        }
    }
}