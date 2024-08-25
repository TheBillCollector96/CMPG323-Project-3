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

        public async Task<Client> CreateClient(Client client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            
            return client;
        }

        public async Task<Client> FindByID(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client;
        }

        public async Task<Client> RemoveClient(Client client)
        {
            _context.Clients.Remove(client);
            return client;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Client> DeleteWithID(Guid? id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);

            return client;
        }

        public async Task<Client> EditClient(Guid id, Client client)
        {
            _context.Update(client);
            await _context.SaveChangesAsync();
            
            return client;
        }

        public async Task<Client> EditClientWithID(Guid? id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client;
        }

        public async Task<IEnumerable<Client>> GetClientAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetDetails(Guid? id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            return client;
        }
    }
}
