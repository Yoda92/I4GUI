using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
            thisMainWindowViewModel = new MainWindowViewModel();
            DataContext = thisMainWindowViewModel;
        }

        private void AddDebtorButton_Click(object sender, RoutedEventArgs e)
        {
            AddDebtorDialog dlg = new AddDebtorDialog();
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                string _Name = dlg.DialogName.Text;
                thisMainWindowViewModel.DebtorList.Add(new Debtor(_Name));
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewDebtorDialog dlg = new ViewDebtorDialog(thisMainWindowViewModel.SelectedDebtor);
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            thisMainWindowViewModel.ExitCommandHandler();
        }
    }
    public class MainWindowViewModel
    {
        XmlSerializer Serializer;
        ObservableCollection<Debtor> _DebtorList;
        string CurrentPath = null;
        Debtor _SelectedDebtor = null;
        FileStream MyStream;
        public ICommand ExitCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public MainWindowViewModel()
        {
            // Create list of debtors
            _DebtorList = new ObservableCollection<Debtor>();

            // Delegate commands
            ExitCommand = new DelegateCommand(ExitCommandHandler);
            SaveCommand = new DelegateCommand(SaveCommandHandler);
            SaveAsCommand = new DelegateCommand(SaveAsCommandHandler);
            OpenCommand = new DelegateCommand(OpenCommandHandler);

            // Create the XML Serializer.
            Serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
        }
        public Debtor SelectedDebtor
        {
            get
            {
                return _SelectedDebtor;
            }
            set
            {
                if(value != null)
                {
                    _SelectedDebtor = value;
                }
            }
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
        public void ExitCommandHandler()
        {
            if (CurrentPath == null)
            {
                SaveAsCommandHandler();
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you want to save changes?", "Exit", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SaveCommandHandler();
                }
            }
            Environment.Exit(0);
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
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
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
            openFileDialog.Filter = "XML file (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                var fileStream = openFileDialog.OpenFile();
                ObservableCollection<Debtor> TempList = (ObservableCollection<Debtor>)Serializer.Deserialize(fileStream);
                DebtorList = TempList;
                fileStream.Close();
            }
        }

    }
}
