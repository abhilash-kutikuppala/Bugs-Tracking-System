using BugsTrackingSystem.Models;
using BugsTrackingSystem.ViewModels;

namespace BugsTrackingSystem.Services
{
    public interface IProjectsService
    {
        Task<IEnumerable<ProjectViewModel>> GetAllProjectsAsync();
        Task<ProjectViewModel> GetProjectByIDAsync(int id);

        Task<ProjectViewModel> CreateProjectAsync(ProjectCreateModel project);

        

    }
}