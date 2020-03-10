using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    public class Debtor : INotifyPropertyChanged
    {
        private ObservableCollection<Debt> _DebtList = new ObservableCollection<Debt>();
        string name;
        int balance;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
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
                NotifyPropertyChanged();
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
