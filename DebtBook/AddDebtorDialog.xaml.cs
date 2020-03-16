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
    /// Interaction logic for AddDebtorDialog.xaml
    /// </summary>
    public partial class AddDebtorDialog : Window
    {
        private AddDebtorDialogModel thisAddDebtorDialogModel;
        public AddDebtorDialog()
        {
            InitializeComponent();
            thisAddDebtorDialogModel = new AddDebtorDialogModel();
            DataContext = thisAddDebtorDialogModel;
            thisAddDebtorDialogModel.RequestAdd += delegate (object sender, EventArgs args) { this.DialogResult = true; };
            thisAddDebtorDialogModel.RequestCancel += delegate (object sender, EventArgs args) { this.DialogResult = false; };
        }
    }

    public class AddDebtorDialogModel
    {
        public event EventHandler RequestAdd;
        public event EventHandler RequestCancel;
        public AddDebtorDialogModel()
        {
            AddCommand = new DelegateCommand(AddCommandHandler);
            CancelCommand = new DelegateCommand(CancelCommandHandler);
        }

        public void AddCommandHandler()
        {
            RequestAdd(this, EventArgs.Empty);
        }

        public void CancelCommandHandler()
        {
            RequestCancel(this, EventArgs.Empty);
        }

        public ICommand AddCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }



    }
}
