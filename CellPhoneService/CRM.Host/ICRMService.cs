using CRM.CommonFiles.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace CRM.Host
{
    [ServiceContract]
    public interface ICRMService
    {

        [OperationContract]
        void SendClient(Client client);

        [OperationContract]
        void SendLine(Line line);

        [OperationContract]
        IEnumerable<ClientType> GetClientTypes();

        [OperationContract]
        void SendCall(Call call);

        [OperationContract]
        void SendSMS(SMS sMS);

        [OperationContract]
        void SendPackageInclude(PackageInclude packageInclude);

        [OperationContract]
        IEnumerable<Client> GetClients();

        [OperationContract]
        IEnumerable<Line> GetLines();

        [OperationContract]
        IEnumerable<Package> GetPackages();

        [OperationContract]
        IEnumerable<Line> GetLinesofClient(Client client);

        [OperationContract]
        IEnumerable<Line> GetLinesByClientId(string Id);

        [OperationContract]
        Package GetInvoce(Line line);

        [OperationContract]
        IEnumerable<Call> GetCallsOfLine(int lineId);

        [OperationContract]
        Client DeleteClient(Client client);

        [OperationContract]
        Line DeleteLine(Line line);
    }
}
