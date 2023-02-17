using BugsTrackingSystem.ViewModels;
using BugsTrackingSystem.Services;
using Microsoft.EntityFrameworkCore;
using BugsTrackingSystem.DAL;
using BugsTrackingSystem.Models;
using System.Net.Http.Headers;

namespace BugsTrackingSystem.Services
{

    public class BugsService : IBugsService
    {
        private readonly BugsResolvingContext _context;

        public BugsService(BugsResolvingContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<BugViewModel>> GetBugsByProjectIDAsync(int projectID)
        {
            return await _context.Bugs.Where(b => b.ProjectID == projectID).Select(b => new BugViewModel
            {
                Description = b.Description,
                ProjectName = FromId(projectID),
                BugState = b.BugState.ToString()

            }).ToListAsync();
        }

        public async Task<BugViewModel> GetBugByBugIDAsync(int bugID)
        {
            var bug = await _context.Bugs.FirstOrDefaultAsync(b => b.BugID == bugID);

            var bugViewModel = new BugViewModel
            {
                Description = bug.Description,
                ProjectName = FromId(bug.ProjectID),
                BugState = bug.BugState.ToString(),

            };

            return bugViewModel;
        }

        public async Task<BugViewModel> CreateBugAsync(BugCreateModel bugModel, int projectId)
        {
            Bug b = new Bug
            {
                Description= bugModel.Description,
                BugState= Models.State.Open,
                ProjectID= projectId,
                BugID= bugModel.BugID
            };
            await _context.Bugs.AddAsync(b);
            await _context.SaveChangesAsync();

            var bViewModel = new BugViewModel
            {
                Description = b.Description,
                BugState=b.BugState.ToString(),
                ProjectName= FromId(projectId),
            };

            return bViewModel;
        }

        private string FromId(int id)
        {
            Project p = _context.Projects.First(p => p.ProjectID == id);
            return p.Name;

        }

    }
}
