using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    interface IDalVat : IDisposable
    {
        //VAT GetAppliedVatById(int? id);
        decimal GetVatValById(int? vatId);
        List<VAT> GetAllVats();
        int GetVatIdByVal(decimal globalVAT);
    }
}
