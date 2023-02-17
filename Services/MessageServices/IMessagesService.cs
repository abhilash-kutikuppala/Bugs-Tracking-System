using BugsTrackingSystem.Models;
using BugsTrackingSystem.ViewModels;

namespace BugsTrackingSystem.Services
{
    public interface IMessagesService
    {
       
        Task<IEnumerable<MessageViewModel>> GetMessagesOfBugByIDAsync(int bugID);

        Task<MessageViewModel> GetMessageByIdAsync(int messageId);
        Task<MessageViewModel> CreateMessageAsync(MessageCreateModel messageModel, int bugId);


    }
}