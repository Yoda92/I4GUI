using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DebtBook
{
    class Debtor
    {
        public struct Debt
        {
            public Debt(int Debt, DateTime Date)
            {
                _Debt = Debt;
                _Date = Date;
            }
            int _Debt { get; set; }
            DateTime _Date { get; set; }
        }

        public List<Debt> DebtList = new List<Debt>();

        public Debtor()
        {
        }

        public void AddDebt(int Debt, DateTime Date)
        {
            Debt NewDebt = new Debt(Debt, Date);
            DebtList.Add(NewDebt);
        }
    }
}
