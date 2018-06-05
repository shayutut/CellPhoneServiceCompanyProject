using CRM.CommonFiles.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRM.UI.ViewModel
{
    public class ClientInfoModel: ViewModelBase
    {
        private Client clientInfo;

        public Client Client
        {
            get { return clientInfo; }
            set { clientInfo = value; }
        }

        private ObservableCollection<ClientType> clientTypes;

        public ObservableCollection<ClientType> ClientTypes
        {
            get { return clientTypes; }
            set { clientTypes = value; }
        }

        private readonly INavigationService _navigationService;
        public RelayCommand NavigateCommand { get; private set; }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        CRMService.CRMServiceClient CRMService ;

        public ClientInfoModel(INavigationService navigationService)
        {
            Client = new Client();
            _navigationService = navigationService;
            CommandInvoke();
            CRMService = new UI.CRMService.CRMServiceClient();
            ClientTypes = new ObservableCollection<ClientType>(CRMService.GetClientTypesAsync().Result);
        }

        private void CommandInvoke()
        {
            SaveCommand = new RelayCommand(() => { CRMService.SendClientAsync(Client); ClearMethod(); });
            ClearCommand = new RelayCommand(() => ClearMethod());
            DeleteCommand = new RelayCommand(() =>
            {
                if (Client != null)
                    CRMService.DeleteClientAsync(Client);
            });

            NavigateCommand = new RelayCommand(NavigateCommandAction);

        }

        private void ClearMethod()
        {
            Client = null;
        }

        private void NavigateCommandAction()
        {
            _navigationService.NavigateTo("LineInformationView");
        }

    }
}
