using BugsTrackingSystem.ViewModels;

namespace BugsTrackingSystem.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync();
    }
}
