using BugsTrackingSystem.ViewModels;
using BugsTrackingSystem.Services;
using Microsoft.EntityFrameworkCore;
using BugsTrackingSystem.DAL;
using BugsTrackingSystem.Models;
using System.Net.Http.Headers;

namespace BugsTrackingSystem.Services
{

    public class ProjectsService : IProjectsService
    {
        private readonly BugsResolvingContext _context;

        public ProjectsService(BugsResolvingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Select(p => new ProjectViewModel
                {
                    Name = p.Name,
                    ProjectID = p.ProjectID,

                })
                .ToListAsync();
        }

        public async Task<ProjectViewModel> GetProjectByIDAsync(int id)
        {
            var p=await _context.Projects.FirstAsync(p => p.ProjectID == id);

            var NewProject = new ProjectViewModel
            {

                Name = p.Name,
                ProjectID = p.ProjectID,

            };
            return NewProject;

        }

        public async Task<ProjectViewModel> CreateProjectAsync(ProjectCreateModel project)
        {
            Project p = new Project
            {
                Name = project.Name,
                ProjectID= project.ProjectID,
               
            };
            _context.Projects.AddAsync(p);
            await _context.SaveChangesAsync();
            return new ProjectViewModel
            {
                Name = project.Name,
                ProjectID = project.ProjectID,
            };
        }


        private async Task<Project> FromIdAsync(int id)
        {
            return await _context.Projects.FirstAsync(p => p.ProjectID == id);
            

        }

    }
}