using CRM.CommonFiles.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.CommonFiles.Interfaces
{
    public interface ICRMRepository
    {
        IEnumerable<ClientType> GetClientTypes();
        IEnumerable<Line> GetLines();
        IEnumerable<Package> GetPackages();
        void AddLine(Line line);
        void AddPackageDetails(PackageInclude packageInclude);
        void AddClient(Client client);
        Client GetClientById(string Id);
        IEnumerable<Line> GetLinesByClientId(string Id);
        void SendCall(Call call);
        void SendCall(SMS sMS);
        IEnumerable<Client> GetClients();
        IEnumerable<Line> GetLinesofClient(Client c);
        IEnumerable<SMS> GetSMSsOfClient(string clintId);
        IEnumerable<Call> GetCallsOfClient(string clintId);
        ClientType GetClientType(Client c);
        IEnumerable<Call> GetCallsOfLine(int lineId);
        IEnumerable<SMS> GetSMSsOfLine(int lineId);
        Package GetPackage(int id);
        Package GetPackage(string name);
        Client DeleteClient(Client client);
        Line DeleteLine(Line client);

        //void ExecuteDbActions<T>(Func<PhoneCompanyContext, T> function);
    }
}

