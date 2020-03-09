using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Serialization;
using Microsoft.Win32;
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
        XmlSerializer Serializer;
        ObservableCollection<Debtor> _DebtorList;
        string CurrentPath = null;
        FileStream MyStream;

        public ICommand ExitCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public MainWindowViewModel()
        {
            // Create list of debtors
            _DebtorList = new ObservableCollection<Debtor>();

            Debtor Debtor1 = new Debtor("Anders Fisker");
            Debtor1.AddDebt(100, new DateTime(1992, 07, 10));

            Debtor Debtor2 = new Debtor("David Bendix");
            Debtor2.AddDebt(-330, new DateTime(2000, 01, 02));
            Debtor2.AddDebt(30, new DateTime(2000, 03, 03));

            Debtor Debtor3 = new Debtor("Christoffer Nørbjerg");
            Debtor3.AddDebt(550, new DateTime(1996, 01, 10));

            DebtorList.Add(Debtor1);
            DebtorList.Add(Debtor2);
            DebtorList.Add(Debtor3);

            ExitCommand = new DelegateCommand(ExitCommandHandler);
            SaveCommand = new DelegateCommand(SaveCommandHandler);
            SaveAsCommand = new DelegateCommand(SaveAsCommandHandler);
            OpenCommand = new DelegateCommand(OpenCommandHandler);

            // Create the XML Serializer.
            Serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
        }
        public ObservableCollection<Debtor> DebtorList
        {
            get
            {
                return _DebtorList;
            }
            set
            {
                _DebtorList.Clear();
                foreach (Debtor ThisDebtor in value)
                {
                    _DebtorList.Add(ThisDebtor);
                }
            }
        }
        void ExitCommandHandler()
        {
            Application.Current.Shutdown();
        }

        void SaveCommandHandler()
        {
            if (CurrentPath == null)
            {
                SaveAsCommandHandler();
            }
            else
            {
                MyStream = File.Create(CurrentPath);
                Serializer.Serialize(MyStream, DebtorList);
                MyStream.Close();
            }
        }
        void SaveAsCommandHandler()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                CurrentPath = saveFileDialog.FileName;
                MyStream = File.Create(CurrentPath);
                Serializer.Serialize(MyStream, DebtorList);
                MyStream.Close();
            }
        }

        void OpenCommandHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var fileStream = openFileDialog.OpenFile();
                ObservableCollection<Debtor> tempagentList = (ObservableCollection<Debtor>)Serializer.Deserialize(fileStream);
                DebtorList = tempagentList;
                fileStream.Close();
            }
        }

    }
}
