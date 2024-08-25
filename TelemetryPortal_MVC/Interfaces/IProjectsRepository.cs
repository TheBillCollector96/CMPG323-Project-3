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

        public Task<Project> CreateProject([Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project);

        public Task<Project> GetDetails(Guid? id);

        public Task<Project> DeleteProject(Guid? id);

        public Task<Project> EditWithID(Guid? id);

        public Task<Project> EditProject(Guid id, [Bind("ProjectId,ProjectName,ProjectDescription,ProjectCreationDate,ProjectStatus,ClientId")] Project project);

        public Task<Project> FindByID(Guid id);

        public Task<Project> RemoveProject(Project project);

        public Task SaveChanges();

        public bool ProjectExistance(Guid id);

    }
}
