using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Prism.Commands;

namespace DebtBook
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel thisMainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel thisMainWindowViewModel = new MainWindowViewModel();
            DataContext = thisMainWindowViewModel;
        }

        private void AddDebtorButton_Click(object sender, RoutedEventArgs e)
        {
            AddDebtorDialog dlg = new AddDebtorDialog();
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                // Set properties
                // var ID_Value = dlg.TextBox0.Text;
                // Add to list thisMainWindowViewModel.agentList.Add(new AgentAssignment.Agent(ID_Value, Name_Value, Speciality_Value, Assignment_Value));
            }
        }

    }

    public class MainWindowViewModel
    {
        Debtor Test = new Debtor();
        public MainWindowViewModel()
        {
            Test.AddDebt(100, new DateTime(1992, 07, 10));
        }
    }
}
