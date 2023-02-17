using BugsTrackingSystem.Services;
using BugsTrackingSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugsTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _serviceMessage;

        public MessagesController(IMessagesService serviceMessage)
        {
            _serviceMessage = serviceMessage;
        }

        [Authorize(Policy = "User")]
       
        [HttpGet("{messageId:int}")]
        public async Task<ActionResult<MessageViewModel>> GetMessageByIdAsync(int messageId)
        {
            return Ok(await _serviceMessage.GetMessageByIdAsync(messageId));
        }
        [HttpPost("Bug/{bugId:int}")]
        public async Task<ActionResult<MessageViewModel>> CreateMessageAsync(MessageCreateModel messageModel, int bugId)
        {
            var msg = await _serviceMessage.CreateMessageAsync(messageModel, bugId);
            if (msg == null)
            {
                return Ok("This bug is already Resolved!");
            }
            else
            {
                return msg;
            }
           
        }

        [HttpGet("Bug/{id:int}")]
        public async Task<ActionResult<IEnumerable<MessageViewModel>>> GetMessagesOfBugByIDAsync(int id)
        {
            return Ok(await _serviceMessage.GetMessagesOfBugByIDAsync(id));
        }
    }
}
