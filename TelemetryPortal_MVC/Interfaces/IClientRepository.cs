using TelemetryPortal_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TelemetryPortal_MVC.Interfaces
{
    public interface IClientRepsitory
    {
        public Task<IEnumerable<Client>> GetClientAsync();

        public Task<Client> GetDetails(Guid? id);

        public Task<Client> CreateClient([Bind("ClientId,ClientName,PrimaryContactEmail,DateOnboarded")] Client client);

        public Task<Client> EditClientWithID(Guid? id);

        public Task<Client> EditClient(Guid id, [Bind("ClientId,ClientName,PrimaryContactEmail,DateOnboarded")] Client client);

        public Task<Client> DeleteWithID(Guid? id);

        public Task<Client> FindByID(Guid id);

        public Task<Client> RemoveClient(Client client);

        public Task SaveChanges();

        public bool CheckClient(Guid id);
    }
}
