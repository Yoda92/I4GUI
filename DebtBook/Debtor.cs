using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DebtBook
{
    public class Debt
    {
        public Debt(int InputValue, DateTime InputDate)
        {
            Value = InputValue;
            Date = InputDate;
        }
        public Debt() {}
        int _Value;
        DateTime _Date;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }
        public string DateOnly
        {
            get
            {
                return _Date.Date.ToShortDateString();
            }
            set{}
        }
    }
    public class Debtor
    {
        private ObservableCollection<Debt> _DebtList = new ObservableCollection<Debt>();
        string name;
        int balance;
        public ObservableCollection<Debt> DebtList
        {
            get
            {
                return _DebtList;
            }
            set
            {
                _DebtList = value;
            }
        }
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
        public Debtor(string _Name, int _Balance)
        {
            Name = _Name;
            Balance = _Balance;
        }

        public void AddDebt(int Debt, DateTime Date)
        {
            DebtList.Add(new Debt(Debt, Date));
            Balance += Debt;
        }
    }
}
