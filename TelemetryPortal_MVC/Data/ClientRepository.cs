using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelemetryPortal_MVC.Interfaces;
using TelemetryPortal_MVC.Models;


namespace TelemetryPortal_MVC.Data
{
    public class ClientRepository : IClientRepsitory
    {
        private readonly TechtrendsContext _context;

        public ClientRepository(TechtrendsContext context)
        {
            _context = context;
        }

        public bool CheckClient(Guid id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }

        public async Task<IActionResult> CreateClient(Client client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            
            return (IActionResult) client;
        }

        public async Task<IActionResult> FindByID(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            return (IActionResult)client;
        }

        public async Task<IActionResult> RemoveClient(Client client)
        {
            _context.Clients.Remove(client);
            return (IActionResult)client;
        }

        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> DeleteWithID(Guid? id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);

            return (IActionResult)client;
        }

        public async Task<IActionResult> EditClient(Guid id, Client client)
        {
            _context.Update(client);
            await _context.SaveChangesAsync();
            
            return (IActionResult)client;
        }

        public async Task<IActionResult> EditClientWithID(Guid? id)
        {
            var client = await _context.Clients.FindAsync(id);
            return (IActionResult)client;
        }

        public async Task<IActionResult> GetClientAsync()
        {
            return (IActionResult)await _context.Clients.ToListAsync();
        }

        public async Task<IActionResult> GetDetails(Guid? id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            return (IActionResult) client;
        }
    }
}
