using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    interface IDalPayment : IDisposable
    {
        void CreatePayment(decimal tot, int methodP, int numTransaction);
        IList<PAYMENT_METHOD> GetAllMethods();
        IList<PAYMENT> GetAllPaymentsByTransacId(int numTransaction);
        string GetMethodNameById(int paymentMethodId);
    }
}
