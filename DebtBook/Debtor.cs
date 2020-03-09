using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DebtBook
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
    public class Debtor
    {
        public List<Debt> DebtList = new List<Debt>();
        string name;
        int balance;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }
        public Debtor()
        {
            Name = null;
            Balance = 0;
        }
        public Debtor(string _Name)
        {
            Name = _Name;
            Balance = 0;
        }

        public void AddDebt(int Debt, DateTime Date)
        {
            DebtList.Add(new Debt(Debt, Date));
            Balance += Debt;
        }
    }
}
