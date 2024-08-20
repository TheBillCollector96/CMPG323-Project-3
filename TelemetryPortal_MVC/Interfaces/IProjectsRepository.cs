using TelemetryPortal_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TelemetryPortal_MVC.Interfaces
{
    public interface IProjectsRepository
    {
        public Task<IEnumerable<Project>> GetProjectAsync();

        public Task<IActionResult> CreateProject([Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project);

        public Task<IActionResult> GetDetails(Guid? id);

        public Task<IActionResult> DeleteProject(Guid? id);

        public Task<IActionResult> EditWithID(Guid? id);

        public Task<IActionResult> EditProject(Guid id, [Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project);

        public Task<Project> FindByID(Guid id);

        public Task<IActionResult> RemoveProject(Project project);

        public void SaveChanges();

        public bool ProjectExistance(Guid id);

    }
}
