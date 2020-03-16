using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Prism.Commands;

namespace DebtBook
{
    /// <summary>
    /// Interaction logic for ViewDebtorDialog.xaml
    /// </summary>
    public partial class ViewDebtorDialog : Window
    {
        ViewDebtorDialogModel thisViewDebtorDialogModel;
        public ViewDebtorDialog(Debtor ThisDebtor)
        {
            InitializeComponent();
            thisViewDebtorDialogModel = new ViewDebtorDialogModel(ThisDebtor);
            DataContext = thisViewDebtorDialogModel;
            thisViewDebtorDialogModel.RequestAdd += delegate(object sender, EventArgs args)
            {
                AddDebtDialog dlg = new AddDebtDialog();
                dlg.Owner = this;
                if (dlg.ShowDialog() == true)
                {
                    int _Value = int.Parse(dlg.DialogValue.Text);
                    DateTime _Date = (DateTime)dlg.DialogDate.SelectedDate;
                    thisViewDebtorDialogModel.ViewDebtor.AddDebt(_Value, _Date);
                }
            };
            thisViewDebtorDialogModel.RequestClose += delegate (object sender, EventArgs args) { this.DialogResult = false; };
        }
    }

    public class ViewDebtorDialogModel
    {
        Debtor _ViewDebtor;
        public event EventHandler RequestAdd;
        public event EventHandler RequestClose;
        public ViewDebtorDialogModel(Debtor ThisDebtor)
        {
            ViewDebtor = new Debtor();
            ViewDebtor = ThisDebtor;
            AddCommand = new DelegateCommand(AddCommandHandler);
            CloseCommand = new DelegateCommand(CloseCommandHandler);
        }

        public void AddCommandHandler()
        {
            RequestAdd(this, EventArgs.Empty);
        }

        public void CloseCommandHandler()
        {
            RequestClose(this, EventArgs.Empty);
        }

        public ICommand AddCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public Debtor ViewDebtor
        {
            get
            {
                return _ViewDebtor;
            }
            set
            {
                _ViewDebtor = value;
            }
        }
    }
    }
