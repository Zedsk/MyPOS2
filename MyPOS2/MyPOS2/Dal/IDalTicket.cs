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
        string GetTicketMessageByIdAndLanguage(int messageId, int languageMessage);
    }
}
