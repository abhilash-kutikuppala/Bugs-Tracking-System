using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BugsTrackingSystem.Services;
using BugsTrackingSystem.ViewModels;
using BugsTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Azure.Messaging;

namespace BugsTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    { 
        private readonly IBugsService _serviceBug;

        public BugsController(IBugsService serviceb)
        {
            _serviceBug = serviceb;
        }

       

        [HttpGet("Project/{id:int}")]
        public async Task<ActionResult<IEnumerable<BugViewModel>>> GetBugsByProjectIDAsync(int id)
        {
            return Ok(await _serviceBug.GetBugsByProjectIDAsync(id));
        }

       
        
        [HttpGet("{bugID:int}")]
        public async Task<ActionResult<BugViewModel>> GetBugByBugIDAsync(int bugID)
        {
            return Ok(await _serviceBug.GetBugByBugIDAsync(bugID));
        }
        [HttpPost("Project/{projectId:int}")]
        public async Task<ActionResult<BugViewModel>> CreateBugAsync(BugCreateModel bugModel, int projectId)
        {
            return Ok(await _serviceBug.CreateBugAsync(bugModel,projectId));
        }
        



    }
}
