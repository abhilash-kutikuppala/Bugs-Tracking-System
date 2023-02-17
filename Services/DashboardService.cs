using BugsTrackingSystem.DAL;
using BugsTrackingSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using BugsTrackingSystem.Models;

namespace BugsTrackingSystem.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly BugsResolvingContext _context;
        public DashboardService(BugsResolvingContext context)
        {
            _context = context;
        }

       
            public async Task<DashboardViewModel> GetDashboardAsync()
            {
                return new DashboardViewModel
                {
                    TotalNoOfProjects = await _context.Projects.CountAsync(),
                    TotalNoOfBugs = await _context.Bugs.CountAsync(),
                    TotalNoOfMessages = await _context.Messages.CountAsync(),
                    BugResolutionRate = await _context.Bugs.Where(b => b.BugState == State.Resolved).CountAsync() / (double)await _context.Bugs.CountAsync()

                };

            }
    }
}
