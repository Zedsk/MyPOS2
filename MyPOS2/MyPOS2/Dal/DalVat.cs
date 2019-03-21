using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    public class DalVat : IDalVat
    {
        #region DB

        private Pos1Entities db;

        public DalVat()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public List<VAT> GetAllVats()
        {
            return db.VATs.ToList();
        }

        public int GetVatIdByVal(decimal globalVAT)
        {
            VAT vat = db.VATs.Where(v => v.appliedVat == globalVAT).Single();
            return vat.idVat;
        }

        public decimal GetVatValById(int? vatId)
        {
            VAT vat = db.VATs.Where(v => v.idVat == vatId).Single();
            return vat.appliedVat;
        }

    }
}