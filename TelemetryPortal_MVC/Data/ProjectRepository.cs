using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelemetryPortal_MVC.Interfaces;
using TelemetryPortal_MVC.Models;

namespace TelemetryPortal_MVC.Data
{
    public class ProjectRepository : IProjectsRepository
    {

        private readonly TechtrendsContext _context;

        public ProjectRepository(TechtrendsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateProject([Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();

            return (IActionResult)project;
        }

        /*public async Task<string> CreateProject(Project project)
        {
            var newProject = new Project
            {
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription,
                ProjectStatus = project.ProjectStatus,
                ClientId = project.ClientId,
            };

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            return "Project Details Saved Successfully";
        }*/

        public async Task<IEnumerable<Project>> GetProjectAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<IActionResult> GetDetails(Guid? id)
        {

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);

            return (IActionResult)project;
        }

        public async Task<IActionResult> DeleteProject(Guid? id)
        {

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);

            return (IActionResult)project;
        }

        public async Task<IActionResult> EditWithID(Guid? id)
        {
            
            var project = await _context.Projects.FindAsync(id);

            return (IActionResult)project;
        }

        public async Task<IActionResult> EditProject(Guid id, [Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();

            return (IActionResult)project;
        }

        public async Task<Project> FindByID(Guid id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<IActionResult> RemoveProject(Project project)
        {
            _context.Projects.Remove(project);

            return (IActionResult)project;
        }

        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public bool ProjectExistance(Guid id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }

    }
}
