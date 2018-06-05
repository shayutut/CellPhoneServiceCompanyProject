using CRM.CommonFiles.Models;
using CRM.UI.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace CRM.UI.ViewModel
{
    public class LineInfoModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        public RelayCommand NavigateCommand { get; private set; }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        CRMService.CRMServiceClient CRMService;
        UIService UIService;

        public LineInfoModel(INavigationService navigationService)
        {
            Line = new Line();
            PackageInclude = new PackageInclude();
            PackageInclude.FavoriteNumbers = new SelectedNumbers();
            _navigationService = navigationService;
            UIService = new UIService();
            CRMService = new UI.CRMService.CRMServiceClient();
            NavigateCommand = new RelayCommand(NavigateCommandAction);
            ComboBoxesInitMethod();
            CommandInvoke();
            IsThreeFriends = Visibility.Collapsed;
            IsFavoriteNumber = Visibility.Collapsed;
        }

        private void ComboBoxesInitMethod()
        {
            Packages = new ObservableCollection<Package>(UIService.GetPackages());
            Clients = new ObservableCollection<Client>(UIService.GetClients());
        }

        private ObservableCollection<Client> _clients;

        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }

        private ObservableCollection<Line> _lines;

        public ObservableCollection<Line> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;

            }
        }
        private Visibility isThreeFriends;

        public Visibility IsThreeFriends
        {
            get { return isThreeFriends; }
            set { isThreeFriends = value; }
        }

        private Client client;

        public Client Client
        {
            get { return client; }
            set
            {
                client = value;
                if (Client != null)
                {
                    Lines = new ObservableCollection<Line>(UIService.GetLinesByClient(Client.Id));
                    OnPropertyChanged(nameof(Lines));
                    line.ClientId = client.Id;
                }
            }
        }

        private Line line;
        public Line Line
        {
            get { return line; }
            set
            {
                if (value != null)
                {
                    line = value;
                }
            }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value;
                if (value != null)
                    line.Number = value;
            }
        }

        private void NavigateCommandAction()
        {
            _navigationService.NavigateTo("ClientInformationView");
        }

        private ObservableCollection<Package> packages;

        public ObservableCollection<Package> Packages
        {
            get { return packages; }
            set { packages = value; }
        }
        private Package package;

        public Package Package
        {
            get { return package; }
            set
            {
                package = value;
                line.PackageId = package.Id;
                if (package.Name == "HappyThreeFriends")
                    IsThreeFriends = Visibility.Visible;
                else IsThreeFriends = Visibility.Collapsed;
                if (package.Name == "FavoriteNumber")
                    IsFavoriteNumber = Visibility.Visible;
                else isFavoriteNumber = Visibility.Collapsed;
                OnPropertyChanged(nameof(IsThreeFriends));
                OnPropertyChanged(nameof(IsFavoriteNumber));
            }
        }

        private Visibility isFavoriteNumber;

        public Visibility IsFavoriteNumber
        {
            get { return isFavoriteNumber; }
            set { isFavoriteNumber = value; }
        }

        private PackageInclude packageInclude;

        public PackageInclude PackageInclude
        {
            get { return packageInclude; }
            set { packageInclude = value; }
        }

        private void CommandInvoke()
        {
            SaveCommand = new RelayCommand(() =>
            {
                CRMService.SendLineAsync(Line);
                packageInclude.Package = line.Package;
                CRMService.SendPackageIncludeAsync(packageInclude);
                ClearMethod();
            });
            ClearCommand = new RelayCommand(() => ClearMethod());
            DeleteCommand = new RelayCommand(() =>
            {
                if (Line != null)
                    CRMService.DeleteLineAsync(Line);
            });
            NavigateCommand = new RelayCommand(NavigateCommandAction);

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            );
        }

        private void ClearMethod()
        {
            Lines.Add(line);
            line = null;
            number = null;
            OnPropertyChanged(nameof(Number));
            OnPropertyChanged(nameof(Lines));
        }
    }
}
