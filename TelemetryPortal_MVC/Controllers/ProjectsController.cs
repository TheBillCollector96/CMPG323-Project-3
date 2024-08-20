using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelemetryPortal_MVC.Data;
using TelemetryPortal_MVC.Interfaces;
using TelemetryPortal_MVC.Models;

namespace TelemetryPortal_MVC.Controllers
{
    public class ProjectsController : Controller
    {
        //private readonly TechtrendsContext _context;
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsController(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _projectsRepository.GetProjectAsync());
        }

        /*[HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return View(await _projectsRepository.GetProjectAsync());
        }*/

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(Guid? id) //The new Details linked to ProjectRepository
        {
            
            return View(await _projectsRepository.GetDetails(id));
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project) //The new Create linked to ProjectRepository
        {
            if (ModelState.IsValid)
            {
                project.ProjectId = Guid.NewGuid();
                /*_context.Add(project);
                await _context.SaveChangesAsync();*/
                await _projectsRepository.CreateProject(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var project = await _context.Projects.FindAsync(id);
            var project = _projectsRepository.EditWithID(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _projectsRepository.EditProject(id, project);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);*/

            var project = await _projectsRepository.DeleteProject(id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var project = await _context.Projects.FindAsync(id);
            var project = _projectsRepository.FindByID(id);
            if (project != null)
            {
                //_context.Projects.Remove(project);
                _projectsRepository.RemoveProject((Project) await project);
            }

            //await _context.SaveChangesAsync();
            _projectsRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(Guid id)
        {
            //return _context.Projects.Any(e => e.ProjectId == id);
            return _projectsRepository.ProjectExistance(id);
        }
    }
}
