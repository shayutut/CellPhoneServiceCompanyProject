using CRM.BL;
using CRM.CommonFiles.Models;
using System.Collections.Generic;

namespace CRM.Host
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CRMService" in both code and config file together.
    public class CRMService : ICRMService
    {
        Manager manager;
        public CRMService()
        {
            manager = new Manager();
        }

        public void SendClient(Client client)
        {
            manager.SendClient(client);
        }

        public IEnumerable<ClientType> GetClientTypes()
        {
            return manager.GetClientTypes();
        }

        public void SendCall(Call call)
        {
            manager.SendCall(call);
        }

        public void SendSMS(SMS sMS)
        {
            manager.SendCall(sMS);
        }

        public IEnumerable<Client> GetClients()
        {
            return manager.GetClients();
        }

        public void SendLine(Line line)
        {
            manager.SendLine(line);
        }

        public void SendPackageInclude(PackageInclude packageInclude)
        {
            manager.AddPackageInclude(packageInclude);
        }

        public IEnumerable<Line> GetLines()
        {
            return manager.GetLines();
        }

        public IEnumerable<Package> GetPackages()
        {
            return manager.GetPackages();
        }

        public IEnumerable<Line> GetLinesofClient(Client client)
        {
            var t = manager.GetLinesofClient(client);
            return t;
        }

        public IEnumerable<Line> GetLinesByClientId(string Id)
        {
            return manager.GetLinesofClient(Id);
        }

        public Package GetInvoce(Line line)
        {
            return manager.WriteReportOfClient(line);
        }

        public IEnumerable<Call> GetCallsOfLine(int lineId)
        {
            return manager.GetCallsOfLine(lineId);
        }

        public Client DeleteClient(Client client)
        {
            return manager.DeleteClient(client);
        }

        public Line DeleteLine(Line line)
        {
            return manager.DeleteLine(line);
        }
    }
}
