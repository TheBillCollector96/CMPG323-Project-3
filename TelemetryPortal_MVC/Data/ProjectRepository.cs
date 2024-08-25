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

        public async Task<Project> CreateProject([Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<IEnumerable<Project>> GetProjectAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetDetails(Guid? id)
        {

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);

            return project;
        }

        public async Task<Project> DeleteProject(Guid? id)
        {

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);

            return project;
        }

        public async Task<Project> EditWithID(Guid? id)
        {
            
            var project = await _context.Projects.FindAsync(id);

            return project;
        }

        public async Task<Project> EditProject(Guid id, [Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project> FindByID(Guid id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<Project> RemoveProject(Project project)
        {
            _context.Projects.Remove(project);

            return project;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public bool ProjectExistance(Guid id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }

    }
}
