using MyPOS2.Data.Entity;
using MyPOS2.Models.Transactions.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS2.Dal
{
    interface IDalTicket : IDisposable
    {
        //int CreateTicket();
        string GetTicketMessageTransByIdAndLanguage(int messageId, int languageMessage);
        List<string> GetListTicketMessageTransByIdAndLanguage(List<int?> idMessage, int languageMessage);
        List<int?> GetListIdTransactionMessage(int transacId);
        List<TRANSACTIONS_MESSAGE> GetListTransactionMessage(int transacId);
        void CreateTransactionMessage(int transacId, int idMessage, int languageMessage);
        IList<int> GetTicket(DateTime date);
        IList<int> GetTicket(int idLanguage);
        IList<int> GetTicket(DateTime dateMin, DateTime dateMax);
        IList<int> GetTicket(DateTime dateMin, DateTime dateMax, int idclient);
        IList<int> GetTicket(DateTime dateMin, DateTime dateMax, decimal total);
        IList<int> GetTicketNbItem(DateTime dateMin, DateTime dateMax, int nbItem);
        int GetLanguageTicketByIdTransac(int idTransac);
        void UpdateTransactionMessageLanguage(int transacId, int languageMessage);
        
    }
}
