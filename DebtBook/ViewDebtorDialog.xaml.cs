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
        }

        private void CloseDebtButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void AddDebtButton_Click(object sender, RoutedEventArgs e)
        {
            AddDebtDialog dlg = new AddDebtDialog();
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                int _Value = int.Parse(dlg.DialogValue.Text);
                DateTime _Date = (DateTime)dlg.DialogDate.SelectedDate;
                thisViewDebtorDialogModel.ViewDebtor.AddDebt(_Value, _Date);
            }
        }
    }

    public class ViewDebtorDialogModel
    {
        Debtor _ViewDebtor;
        public ViewDebtorDialogModel(Debtor ThisDebtor)
        {
            ViewDebtor = new Debtor();
            ViewDebtor = ThisDebtor;
        }
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
