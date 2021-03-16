using Bookstore.Core.Models.Authentication;
using Bookstore.Core.Models.Users;
using System.Threading.Tasks;

namespace Bookstore.Core.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<AuthenticateResponse> SignInAsync(AuthenticateRequest request);

        public Task SignOutAsync(string email);

        public Task SignUpAsync(UserModel model);

        public Task ConfirmEmailAsync(string email, string token);

        public Task ForgotPasswordAsync(string email);

        public Task<AuthenticateResponse> RefreshTokenAsync(string accessToken, string refreshToken);
    }
}
