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
    /// Interaction logic for AddDebtDialog.xaml
    /// </summary>
    public partial class AddDebtDialog : Window
    {
        private AddDebtDialogModel thisAddDebtDialogModel;
        public AddDebtDialog()
        {
            InitializeComponent();
            thisAddDebtDialogModel = new AddDebtDialogModel();
            DataContext = thisAddDebtDialogModel;
            thisAddDebtDialogModel.RequestAdd += delegate(object sender, EventArgs args) { this.DialogResult = true; };
            thisAddDebtDialogModel.RequestCancel += delegate (object sender, EventArgs args) { this.DialogResult = false; };
        }
    }

    public class AddDebtDialogModel
    {
        public event EventHandler RequestAdd;
        public event EventHandler RequestCancel;
        public AddDebtDialogModel()
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
