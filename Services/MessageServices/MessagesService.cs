using BugsTrackingSystem.ViewModels;
using BugsTrackingSystem.Services;
using Microsoft.EntityFrameworkCore;
using BugsTrackingSystem.DAL;
using BugsTrackingSystem.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace BugsTrackingSystem.Services
{

    public class MessagesService : IMessagesService
    {
        private readonly BugsResolvingContext _context;

        public MessagesService(BugsResolvingContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<MessageViewModel>> GetMessagesOfBugByIDAsync(int bugID)
        {
            return await _context.Messages.Where(m => m.BugID == bugID).Select(m => new MessageViewModel
            {
                Flag = m.Flag,
                Text = m.Text,


            }).ToListAsync();

        }

        public async Task<MessageViewModel> GetMessageByIdAsync(int messageId)
        {
            var msg=  await _context.Messages.FirstAsync(m => m.MessageID == messageId);
            var msgViewModel = new MessageViewModel
            {
                Flag = msg.Flag,
                Text = msg.Text,

            };
            return msgViewModel;
        }

        public async Task<MessageViewModel> CreateMessageAsync(MessageCreateModel messageModel, int bugId)
        {
            var msg = new Message
            {
                Flag = messageModel.Flag,
                Text = messageModel.Text,
                BugID= bugId,
                MessageID= messageModel.MessageID

            };

            var bug = await _context.Bugs.FirstAsync(b => b.BugID == bugId);
            if(bug.BugState==State.Resolved)
            {
                return null;
            }

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
            if (msg.Flag)
            {
               
                bug.BugState = State.Resolved;
              
                await _context.SaveChangesAsync();
            }
            else
            {
                
                bug.BugState = State.Working;
                await _context.SaveChangesAsync();
            }
            var msgViewModel = new MessageViewModel
            {
                Flag = messageModel.Flag,
                Text = messageModel.Text,

            };
            return msgViewModel;

        }

        private string FromId(int id)
        {
            Project p = _context.Projects.First(p => p.ProjectID == id);
            return p.Name;

        }

    }
}