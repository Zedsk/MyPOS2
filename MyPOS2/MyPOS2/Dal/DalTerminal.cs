using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Dal
{
    public class DalTerminal : IDalTerminal
    {
        #region DB

        private Pos1Entities db;

        public DalTerminal()
        {
            db = new Pos1Entities();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public List<TERMINAL> GetAllTerminals()
        {
            return db.TERMINALs.ToList();
        }

        public TERMINAL GetTerminalById(int id)
        {
            return db.TERMINALs.Where(t => t.idTerminal == id).Single();
        }

        public int GetTerminalIdByDate()
        {
            CASH_BOTTOM_DAY cashDay = db.CASH_BOTTOM_DAYs.Where(c => c.dateDay == DateTime.Today).Single();
            return cashDay.terminalId;
        }

        public IList<string> GetAllTerminalNames()
        {
            return db.TERMINALs.Select(t => t.nameTerminal).ToList();
        }
    }
}