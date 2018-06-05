using CRM.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Common.Interfaces
{
    public interface ICRMRepository
    {
        IEnumerable<ClientType> GetClientTypes();

        void AddClient(Client client);

        Client GetClientById(string Id);

        IEnumerable<Line> GetLinesByClientId(string Id);
        void SendCall(Call call);
        void SendCall(SMS sMS);
        IEnumerable<Client> GetClients();
    }
}
