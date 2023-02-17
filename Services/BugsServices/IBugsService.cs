using BugsTrackingSystem.Models;
using BugsTrackingSystem.ViewModels;

namespace BugsTrackingSystem.Services
{
    public interface IBugsService
    {
        Task<IEnumerable<BugViewModel>> GetBugsByProjectIDAsync(int projectId);
        Task<BugViewModel> GetBugByBugIDAsync(int bugID);
        Task<BugViewModel> CreateBugAsync(BugCreateModel bugModel, int projectId);
        

    }
}