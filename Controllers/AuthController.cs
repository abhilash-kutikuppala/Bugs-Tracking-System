using BugsTrackingSystem.ViewModels;
using BugsTrackingSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugsTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            return Ok(await _authService.Login(viewModel));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateViewModel viewModel)
        {
            await _authService.Register(viewModel);
            return Ok();
        }
    }
}
