using CRM.CommonFiles.Models;
using CRM.UI.Services;
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
    public class CallOrSMSModel
    {
        SimulatorService simulatorService;

        private Call call;

        public Call Call
        {
            get { return call; }
            set { call = value; }
        }

        private bool isSMS;

        public bool IsSMS
        {
            get { return isSMS; }
            set { isSMS = value; }
        }


        private ObservableCollection<Line> lines;

        public ObservableCollection<Line> Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        public int Min { get; set; }
        public int Max { get; set; }

        private int numberOfCalls;

        public int NumberOfCalls
        {
            get { return numberOfCalls; }
            set { numberOfCalls = value; }
        }

        private ObservableCollection<Client> clients;

        public ObservableCollection<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }

        private readonly INavigationService _navigationService;
        public RelayCommand NavigateCommand { get; private set; }

        public ICommand SimulateCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public bool IsSMS1 { get => isSMS; set => isSMS = value; }

        public CallOrSMSModel(INavigationService navigationService)
        {
            simulatorService = new SimulatorService();
            Call = new Call();
            _navigationService = navigationService;
            CommandInvoke();
            Clients = new ObservableCollection<Client>(simulatorService.GetClients());
        }

        private void CommandInvoke()
        {
            SimulateCommand = new RelayCommand(() => { Simulate(); });
            LogOutCommand = new RelayCommand(() => ClearMethod());
            //DeleteCommand = new RelayCommand(() =>

            NavigateCommand = new RelayCommand(NavigateCommandAction);
        }

        private void Simulate()
        {
            simulatorService.SendCalls(Call, NumberOfCalls, Min, Max);
            ClearMethod();
        }

        private void NavigateCommandAction()
        {
            _navigationService.NavigateTo("LogInView");
        }

        private void ClearMethod()
        {
            Call = new Call();
            Max = 0;
            Min = 0;
            numberOfCalls = 0;
        }

        public void ChangeClient(Client client)
        {
            Lines = new ObservableCollection<Line>(client.Lines);
        }
    }
}
