using CRM.CommonFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Simulator.Services
{
    public class SimulatorService
    {
        CRMService.CRMServiceClient CRMService;
        public SimulatorService()
        {
            CRMService = new Simulator.CRMService.CRMServiceClient();
        }

        public IEnumerable<Client> GetClients()
        {
            return CRMService.GetClientsAsync().Result;
        }

        public async Task<IEnumerable<Line>> GetLinesofClient(Client c)
        {
            var t = await CRMService.GetLinesofClientAsync(c);
            return t;
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
        public void SendSMSs(SMS sms, int numberofcalls)
        {
            for (int i = 0; i < numberofcalls; i++)
            {
                CRMService.SendSMSAsync(sms);
            }
        }

        public async Task<Package> GetInvoce(Line line)
        {
           return await CRMService.GetInvoceAsync(line);
        }
    }
}
