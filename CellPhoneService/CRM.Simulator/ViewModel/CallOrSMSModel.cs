using CRM.CommonFiles.Models;
using CRM.Simulator.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace CRM.Simulator.ViewModel
{
    public class CallOrSMSModel : INotifyPropertyChanged
    {
        SimulatorService simulatorService;

        public event PropertyChangedEventHandler PropertyChanged;

        private Call call;

        public Call Call
        {
            get { return call; }
            set { call = value; }
        }

        private Line line;

        public Line Line
        {
            get { return line; }
            set
            {
                line = value;
                if (value != null)
                {
                    IsLineChoosed = Visibility.Visible;
                    OnPropertyChanged(nameof(IsLineChoosed));
                    line.ClientId = Client.Id;
                    call.LineId = line.Id;
                }
            }
        }


        private bool isSMS;

        public bool IsSMS
        {
            get { return isSMS; }
            set { isSMS = value; }
        }

        public Visibility IsLineChoosed { get; set; }

        private ObservableCollection<Line> lines;

        public ObservableCollection<Line> Lines
        {
            get { return lines; }
            set
            {
                lines = value;
                OnPropertyChanged(nameof(Lines));
            }
        }

        private Client _client;

        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value;
                if (Client != null)
                    Task.Factory.StartNew(() =>
                    {
                        Lines = new ObservableCollection<Line>(simulatorService.GetLinesofClient(Client).Result);
                    });
                //OnPropertyChanged(nameof(IsLineChoosed));
            }
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

        private string recomendation;

        public string Recomendation
        {
            get { return recomendation; }
            set
            {
                recomendation = value;
                if (value != null)
                    OnPropertyChanged(nameof(Recomendation));
            }
        }

        public ICommand SimulateCommand { get; set; }
        public ICommand GetInvoceCommand { get; set; }

        public bool IsSMS1 { get => isSMS; set => isSMS = value; }

        public CallOrSMSModel()
        {
            simulatorService = new SimulatorService();
            Call = new Call();
            CommandInvoke();
            Clients = new ObservableCollection<Client>(simulatorService.GetClients());
            Lines = new ObservableCollection<Line>();
            IsLineChoosed = Visibility.Collapsed;
        }

        private void CommandInvoke()
        {
            SimulateCommand = new RelayCommand(() => { Simulate(); });
            GetInvoceCommand = new RelayCommand(() => { GetInvoce(Line); });
        }

        private void GetInvoce(Line line)
        {
            Task.Factory.StartNew(() =>
            {
                line.Client = Client;
                var p = simulatorService.GetInvoce(line).Result;
                if (p != null)
                    Recomendation = $"{p.Name} is the best package for you, system recomedation:)";
            });
        }

        private void Simulate()
        {
            if (!isSMS)
            {
                simulatorService.SendCalls(Call, NumberOfCalls, Min, Max);
            }
            else
            {
                simulatorService.SendSMSs(new SMS() { Line = call.Line, DestinaionNumber = call.DestinaionNumber }, NumberOfCalls);
            }
            ClearMethod();

        }


        private void ClearMethod()
        {
            NumberOfCalls = 0;
            Min = 0;
            Max = 0;
            OnPropertyChanged(nameof(Min));
            OnPropertyChanged(nameof(Max));
            OnPropertyChanged(nameof(NumberOfCalls));
            OnPropertyChanged(nameof(Call));
            Call = new Call();
            Max = 0;
            Min = 0;
            numberOfCalls = 0;
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
    }
}