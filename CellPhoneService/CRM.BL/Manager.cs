using CRM.CommonFiles.Interfaces;
using CRM.CommonFiles.Models;
using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BL
{
    public class Manager
    {
        ICRMRepository cRMRepository;
        ReportManager ReportManager;
        public Manager()
        {
            cRMRepository = new CRMRepository();
            ReportManager = new ReportManager();
            //WriteReportOfClient(cRMRepository.GetClientById("316464320"));
        }
        public Manager(ICRMRepository repo)
        {
            cRMRepository = repo;
        }

        public void SendCall(Call call)
        {
            cRMRepository.SendCall(call);
        }

        public void SendCall(SMS sMS)
        {
            cRMRepository.SendCall(sMS);
        }

        public void SendClient(Client client)
        {
            cRMRepository.AddClient(client);
        }

        public void SendLine(Line line)
        {
            cRMRepository.AddLine(line);
        }

        public IEnumerable<Line> GetLines()
        {
            var f = cRMRepository.GetLines().ToList();
            return f;
        }

        public IEnumerable<ClientType> GetClientTypes()
        {
            var f = cRMRepository.GetClientTypes().ToList();
            return f;
        }

        public IEnumerable<Line> GetLinesofClient(Client c)
        {
            return cRMRepository.GetLinesofClient(c);
        }

        public IEnumerable<Line> GetLinesofClient(string clienId)
        {
            return cRMRepository.GetLinesByClientId(clienId);
        }

        public IEnumerable<Client> GetClients()
        {
            var f = cRMRepository.GetClients().ToList();
            return f;
        }

        public void AddPackageInclude(PackageInclude packageInclude)
        {
            cRMRepository.AddPackageDetails(packageInclude);
        }


        public IEnumerable<Package> GetPackages()
        {
            var f = cRMRepository.GetPackages().ToList();
            return f;
        }

        public Client DeleteClient(Client client)
        {
            return cRMRepository.DeleteClient(client);
        }

        public Line DeleteLine(Line line)
        {
            return cRMRepository.DeleteLine(line);
        }

        public IEnumerable<Call> GetCallsOfLine(int lineId)
        {
            return cRMRepository.GetCallsOfLine(lineId);
        }
        public Package WriteReportOfClient(Line line)
        {
            var calls = cRMRepository.GetCallsOfLine(line.Id).ToList();
            var smss = cRMRepository.GetSMSsOfLine(line.Id).ToList();
            //var lines = cRMRepository.GetLinesByClientId(client.Id).ToList();
            var type = cRMRepository.GetClientType(line.Client);
            var c = cRMRepository.GetClientById(line.ClientId);
            //c.Lines = cRMRepository.GetLinesByClientId(c.Id).ToList();
            return cRMRepository.GetPackage(ReportManager.WriteReportOfClient(c, calls, smss, line, type, GetPackage(line.PackageId)));




            //
            //cRMRepository.ExecuteDbActions((cc) => { cRMRepository.Sendcallwithcontext(cc,calls[0]);
            //...
            //});
            //
        }
        public Package GetPackage(int id)
        {
            return cRMRepository.GetPackage(id);
        }
    }
}
