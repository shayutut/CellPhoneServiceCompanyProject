using CRM.CommonFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public class SimulatorService
    {
        CRMService.CRMServiceClient CRMService;
        Client[] _clients;
        public SimulatorService()
        {
            CRMService = new App1.CRMService.CRMServiceClient();
        }

        public IEnumerable<string> GetClientsIds()
        {
            _clients = CRMService.GetClientsAsync().Result;
            if (_clients != null)
            {
                return _clients.Select(c => c.Id);
            }
            else
            {
                return new List<string>();
            }
        }

        public IEnumerable<string> GetLines(string clientId)
        {
            var client = _clients.FirstOrDefault(c => c.Id == clientId);
            if (client != null)
            {
                return client.Lines.Select(l => l.Number);
            }
            else
            {
                return new List<string>();
            }
        }

            public void SendCalls(Call call, int numberofcalls, int min = 0, int max = 0)
            {
                Random random = new Random();
                for (int i = 0; i < numberofcalls; i++)
                {
                    call.Dureation = random.Next(min, max);
                    CRMService.SendCallAsync(call);
                }
            }
        }
    }
