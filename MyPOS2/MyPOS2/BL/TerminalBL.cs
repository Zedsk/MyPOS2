using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class TerminalBL
    {
        internal static TERMINAL FindTerminalById(int id)
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                return dal.GetTerminalById(id);
            }
        }

        internal static IList<string> FindTerminalsNames()
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                List<TERMINAL> terminals = dal.GetAllTerminals();
                List<string> terminalsNames = new List<string>();
                foreach (var t in terminals)
                {
                    terminalsNames.Add(t.nameTerminal);
                }
                return terminalsNames;
            }
        }

        internal static IList<TERMINAL> FindTerminalsList()
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                return dal.GetAllTerminals();
            }
        }

        internal static int FindTerminalIdByDate()
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                return dal.GetTerminalIdByDate();
            }
        }

        internal static int CheckTerminalName()
        {
            using (IDalTerminal dal = new DalTerminal())
            {
                string T = System.Net.Dns.GetHostName();
                //IList<string> listNames = dal.GetAllTerminalNames();
                List<TERMINAL> terminals = dal.GetAllTerminals();
                foreach (var item in terminals)
                {
                    if (item.nameTerminal == T)
                    {
                        return item.idTerminal;
                    }
                }
                return 0;
            }
        }
    }
}