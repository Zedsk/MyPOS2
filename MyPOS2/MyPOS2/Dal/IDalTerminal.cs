using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    interface IDalTerminal : IDisposable
    {
        List<TERMINAL> GetAllTerminals();
        TERMINAL GetTerminalById(int id);
        int GetTerminalIdByDate();
        IList<string> GetAllTerminalNames();
    }
}
