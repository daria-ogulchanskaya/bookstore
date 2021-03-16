using static Bookstore.Shared.Constants.Constants.AppSettings;
using static Bookstore.Shared.Enums.Enums;
using Bookstore.Core.Models.Authentication;
using Bookstore.Core.Models.Users;
using Bookstore.Core.Services.Interfaces;
using Bookstore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading.Tasks;
using Bookstore.Core.Exceptions;
using Bookstore.Core.Providers;
using System.Web;
using AutoMapper;
using Microsoft.Extensions.Options;
using Bookstore.Shared.Models.Settings;

namespace Bookstore.Core.Services
{
    public class AccountService : IAccountService
    {
        private const string TOKEN_NAME = "token";
        private const string EMAIL_NAME = "email";

        private readonly UserManager<User> _userManager;
        private readonly JwtProvider _jwtProvider;
        private readonly EmailProvider _emailProvider;
        private readonly PasswordGenerator _generator;
        private readonly IMapper _mapper;
        private readonly AuthSettings _auth;
        private readonly EmailConfirmationSettings _emailConfirmation;

        public AccountService(UserManager<User> userManager,
                              JwtProvider jwtProvider,
                              EmailProvider emailProvider,
                              PasswordGenerator generator,
                              IMapper mapper,
                              IOptions<AuthSettings> auth,
                              IOptions<EmailConfirmationSettings> emailConfirmation)
        {
            _emailConfirmation = emailConfirmation.Value;
            _auth = auth.Value;
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _emailProvider = emailProvider;
            _generator = generator;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> SignInAsync(AuthenticateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.INVALID_CREDENTIALS);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound,
                    ExceptionMessage.USER_NOT_FOUND);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.INVALID_CREDENTIALS);
            }

            var accessToken = _jwtProvider.GenerateAccessToken(_mapper.Map<UserModel>(user), roles);
            var refreshToken = _jwtProvider.GenerateRefreshToken();

            await SetRefreshTokenAsync(user, refreshToken);
            
            var response = new AuthenticateResponse(accessToken, refreshToken);

            return response;
        }

        public async Task SignOutAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound, 
                    ExceptionMessage.USER_NOT_FOUND);
            }

            await _userManager.RemoveAuthenticationTokenAsync(user, _auth.Issuer, _auth.TokenName);
        }

        public async Task SignUpAsync(UserModel model)
        {
            var user = _mapper.Map<User>(model);

            if (await EmailExistsAsync(model.Email))
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.EMAIL_ALREADY_EXISTS);
            }
            if (await UsernameExistsAsync(model.Username))
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.USERNAME_ALREADY_EXISTS);
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new ServerException(HttpStatusCode.BadRequest, 
                    ExceptionMessage.CREATE_USER_FAILED);
            }

            await _userManager.AddToRoleAsync(user, RoleType.Client.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string encoded = HttpUtility.UrlEncode(token);

            var uriBuilder = new UriBuilder(_emailConfirmation.Url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[EMAIL_NAME] = model.Email;
            query[TOKEN_NAME] = encoded;

            uriBuilder.Query = query.ToString();
            string uriString = uriBuilder.ToString();

            await _emailProvider.SendEmailConfirmationMessageAsync(model.Email, uriString);
        }

        public async Task ConfirmEmailAsync(string email, string token)
        {
            string decoded = HttpUtility.UrlDecode(token);

            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound, 
                    ExceptionMessage.USER_NOT_FOUND);
            }

            var result = await _userManager.ConfirmEmailAsync(user, decoded);
            if (!result.Succeeded)
            {
                throw new ServerException(HttpStatusCode.BadRequest, 
                    result.Errors.ToString());
            }
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound, 
                    ExceptionMessage.USER_NOT_FOUND);
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string password = _generator.GeneratePassword(TempPassword.SIZE);

            await _userManager.ResetPasswordAsync(user, token, password);
            await _emailProvider.SendPasswordResetMessageAsync(email, password);
        }

        public async Task<AuthenticateResponse> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            var validatedToken = _jwtProvider.ValidateToken(accessToken);

            var user = await _userManager.FindByEmailAsync(validatedToken.Payload.Sub);
            if (user is null)
            {
                throw new ServerException(HttpStatusCode.NotFound, 
                    ExceptionMessage.USER_NOT_FOUND);
            }

            var roles = await _userManager.GetRolesAsync(user);

            await CheckRefreshTokenAsync(user, refreshToken);
            
            accessToken = _jwtProvider.GenerateAccessToken(_mapper.Map<UserModel>(user), roles);
            refreshToken = _jwtProvider.GenerateRefreshToken();

            await SetRefreshTokenAsync(user, refreshToken);

            var response = new AuthenticateResponse(accessToken,refreshToken);

            return response;
        }

        private async Task CheckRefreshTokenAsync(User user, string refreshToken)
        {
            string token = await _userManager.GetAuthenticationTokenAsync(user,
                                                                          _auth.Issuer,
                                                                          _auth.TokenName);

            if (token != refreshToken)
            {
                throw new ServerException(HttpStatusCode.BadRequest,
                    ExceptionMessage.INVALID_TOKEN);
            }
        }

        private async Task SetRefreshTokenAsync(User user, string refreshToken) => 
            await _userManager.SetAuthenticationTokenAsync(user,
                                                           _auth.Issuer,
                                                           _auth.TokenName,
                                                           refreshToken);

        private async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user is not null;
        }

        private async Task<bool> UsernameExistsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user is not null;
        }
    }
}
