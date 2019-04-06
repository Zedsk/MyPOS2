using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    interface IDalTransaction : IDisposable
    {
        #region Transaction
        int CreateTransaction(int terminal, string vendorId, int shop, int customer);
        List<TRANSACTION_DETAILS> GetAllDetailsByTransactionId(int id);
        TRANSACTIONS GetTransactionById(int transactionId);
        void CreateDetail(PRODUCT prod, int transactionId, int terminalId, decimal vat);
        void CreateDetail(PRODUCT prod, int transactionId, int terminalId, decimal vat, string nameProd);
        void EditQtyToDetailById(int id, int qty);
        void DeleteDetail(int id);
        void UpdateTransaction(int transactionId, decimal globalTotal, decimal? discountG);
        void CancelTransactionById(int transactionId);
        //void UpdateTransactionMessageId(int transactionId, int idTicket);
        void CloseTransaction(int transac);
        void CloseTransaction(int transac, DateTime date);
        IList<SPP_TransactionsDay_Result> GetAllTransactionDay(DateTime date);
        #endregion
    }
}
