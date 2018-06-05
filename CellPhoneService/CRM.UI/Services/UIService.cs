using CRM.CommonFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.UI.Services
{
    public class UIService
    {
        CRMService.CRMServiceClient CRMService;
        public UIService()
        {
            CRMService = new UI.CRMService.CRMServiceClient();
        }

        public IEnumerable<Client> GetClients()
        {
            var clientsTask = Task.Factory.StartNew(() => CRMService.GetClientsAsync());
            return clientsTask.Result.Result;
        }
        public IEnumerable<Line> GetLinesByClient(string id)
        {
            var clientsTask = Task.Factory.StartNew(() => CRMService.GetLinesByClientIdAsync(id));
            return clientsTask.Result.Result;
        }
        public IEnumerable<Package> GetPackages()
        {
            var clientsTask = Task.Factory.StartNew(() => CRMService.GetPackagesAsync());
            return clientsTask.Result.Result;
        }
    }
}
