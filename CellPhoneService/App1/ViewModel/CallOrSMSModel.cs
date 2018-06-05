using App1.Services;
using CRM.CommonFiles.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App1.ViewModel
{
    public class CallOrSMSModel
    {
        SimulatorService simulatorService;
        private Call call;
        private bool isSMS;
        private ObservableCollection<string> clients = new ObservableCollection<string>();
        private ObservableCollection<string> lines;
        private int numberOfCalls;
        private string _selectedClient;

        public ICommand SimulateCommand { get; set; }
        public ICommand OnClientChangedCommand { get; set; }

        public CallOrSMSModel()
        {
            simulatorService = new SimulatorService();
            Call = new Call();
            CommandInvoke();
            Clients = new ObservableCollection<string>(simulatorService.GetClientsIds());
        }

        public Call Call
        {
            get { return call; }
            set { call = value; }
        }
        public bool IsSMS
        {
            get { return isSMS; }
            set { isSMS = value; }
        }
        public ObservableCollection<string> Lines
        {
            get { return lines; }
            set { lines = value; }
        }
        public int Min { get; set; }
        public int Max { get; set; }
        public int NumberOfCalls
        {
            get { return numberOfCalls; }
            set { numberOfCalls = value; }
        }
        public ObservableCollection<string> Clients
        {
            get { return clients; }
            set { clients = value; }
        }
        public bool IsSMS1 { get => isSMS; set => isSMS = value; }
        public string SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                Lines = new ObservableCollection<string>(simulatorService.GetLines(_selectedClient));
            }
        }

        private void CommandInvoke()
        {
            SimulateCommand = new RelayCommand(() => { Simulate(); });
            OnClientChangedCommand = new RelayCommand(ChangeClient);
        }

        private void ChangeClient()
        {
            Lines = new ObservableCollection<string>(simulatorService.GetLines(_selectedClient));
        }

        private void Simulate()
        {
            simulatorService.SendCalls(Call, NumberOfCalls, Min, Max);
            ClearMethod();
        }
        private void ClearMethod()
        {
            Call = new Call();
            Max = 0;
            Min = 0;
            numberOfCalls = 0;
        }
    }
}
