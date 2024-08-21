using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelemetryPortal_MVC.Data;
using TelemetryPortal_MVC.Interfaces;
using TelemetryPortal_MVC.Models;

namespace TelemetryPortal_MVC.Controllers
{
    public class ClientsController : Controller
    {
        //private readonly TechtrendsContext _context;
        private readonly IClientRepsitory _clientRepsitory;

        public ClientsController(IClientRepsitory clientRepsitory)
        {
            _clientRepsitory = clientRepsitory;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            //await _context.Clients.ToListAsync()
            return View(_clientRepsitory.GetClientAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);*/
            var client = _clientRepsitory.GetDetails(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientName,PrimaryContactEmail,DateOnboarded")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.ClientId = Guid.NewGuid();

                /*_context.Add(client);
                await _context.SaveChangesAsync();*/

                _clientRepsitory.CreateClient(client);

                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var client = await _context.Clients.FindAsync(id);*/
            var client = _clientRepsitory.EditClientWithID(id);

            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ClientId,ClientName,PrimaryContactEmail,DateOnboarded")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*_context.Update(client);
                    await _context.SaveChangesAsync();*/
                    _clientRepsitory.EditClient(id, client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);*/
            var client = _clientRepsitory.DeleteWithID(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            /*var client = await _context.Clients.FindAsync(id);*/
            var client = _clientRepsitory.FindByID(id);
            if (client != null)
            {
                /*_context.Clients.Remove(client);*/
                _clientRepsitory.RemoveClient((Client) await client);
            }

            //await _context.SaveChangesAsync();
            _clientRepsitory.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(Guid id)
        {

            return _clientRepsitory.CheckClient(id);
        }
    }
}
