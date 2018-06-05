using CRM.CommonFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.UI.Services
{
    public class SimulatorService
    {
        CRMService.CRMServiceClient CRMService;
        public SimulatorService()
        {
            CRMService = new UI.CRMService.CRMServiceClient();
        }

        public IEnumerable<Client> GetClients()
        {
            return CRMService.GetClientsAsync().Result;
        }

        public void SendCalls(Call call, int numberofcalls, int min = 0, int max = 0)
        {
            Random random=new Random();
            for (int i = 0; i < numberofcalls; i++)
            {
                call.Dureation = random.Next(min,max);
                CRMService.SendCallAsync(call);
            }
        }
    }
}
