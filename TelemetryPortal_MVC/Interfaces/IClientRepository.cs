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
        public Task<IActionResult> GetClientAsync();

        public Task<IActionResult> GetDetails(Guid? id);

        public Task<IActionResult> CreateClient([Bind("ClientId,ClientName,PrimaryContactEmail,DateOnboarded")] Client client);

        public Task<IActionResult> EditClientWithID(Guid? id);

        public Task<IActionResult> EditClient(Guid id, [Bind("ClientId,ClientName,PrimaryContactEmail,DateOnboarded")] Client client);

        public Task<IActionResult> DeleteWithID(Guid? id);

        public Task<IActionResult> FindByID(Guid id);

        public Task<IActionResult> RemoveClient(Client client);

        public void SaveChanges();

        public bool CheckClient(Guid id);
    }
}
