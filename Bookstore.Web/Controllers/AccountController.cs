using System.Threading.Tasks;
using Bookstore.Core.Models.Authentication;
using Bookstore.Core.Models.Users;
using Bookstore.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Web.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _accessor;

        public AccountController(IAccountService accountService,
                                 IHttpContextAccessor accessor)
        {
            _accountService = accountService;
            _accessor = accessor;
        }

        [HttpPost("sign-in")]
        public async Task<AuthenticateResponse> SignInAsync(AuthenticateRequest request) => 
            await _accountService.SignInAsync(request);

        [HttpPost("sign-up")]
        public async Task SignUpAsync(UserModel model) => 
            await _accountService.SignUpAsync(model);

        [HttpPost("sign-out/{email}")]
        public async Task SignOutAsync([FromRoute] string email) => 
            await _accountService.SignOutAsync(email);

        [HttpPost("forgot-password")]
        public async Task ForgotPasswordAsync(string email) => 
           await _accountService.ForgotPasswordAsync(email);

        [HttpPost("confirm-email")]
        public async Task ConfirmEmailAsync(string email, string token) =>
            await _accountService.ConfirmEmailAsync(email, token);

        [HttpGet("refresh-token")]
        public async Task RefreshTokenAsync(RefreshTokenRequestModel request) => 
            await _accountService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);
    }
}
