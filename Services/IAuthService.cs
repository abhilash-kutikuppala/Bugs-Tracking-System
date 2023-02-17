using System.Threading.Tasks;
using BugsTrackingSystem.ViewModels;

namespace BugsTrackingSystem.Services
{
    public interface IAuthService
    {
        Task Register(UserCreateViewModel user);
        Task<JwtViewModel> Login(LoginViewModel loginViewModel);
    }
}