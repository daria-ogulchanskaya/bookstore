using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using Bookstore.Core.Exceptions;
using Bookstore.Core.Models.Users;
using Bookstore.Shared.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static Bookstore.Shared.Constants.Constants.AppSettings;

namespace Bookstore.Core.Providers
{
    public class JwtProvider
    {
        private const int TOKEN_SIZE = 32;

        private readonly TokenValidationParameters _parameters;
        private readonly SymmetricSecurityKey _key;
        private readonly AuthSettings _auth;

        public JwtProvider(IOptions<AuthSettings> auth)
        {
            _auth = auth.Value;
            _key = _auth.GetKey();

            _parameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _auth.Issuer,

                ValidateAudience = true,
                ValidAudience = _auth.Audience,

                IssuerSigningKey = _key
            };
        }

        public string GenerateAccessToken(UserModel user, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Name", user.Name),
                new Claim("Surname", user.Surname)
            };

            claims.AddRange(roles.Select(x => new Claim("role", x)));

            var claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            const string algorithm = SecurityAlgorithms.HmacSha256;
            var credentials = new SigningCredentials(_key, algorithm);

            var token = new JwtSecurityToken(
                _auth.Issuer,
                _auth.Audience,
                claimsIdentity.Claims,
                notBefore: DateTime.Now,
                expires:DateTime.Now.AddSeconds(_auth.TokenLifeTime),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[TOKEN_SIZE];

            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        public JwtSecurityToken ValidateToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(accessToken, _parameters, out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwtToken 
                || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256)
                || jwtToken is null)
            {
                throw new ServerException(HttpStatusCode.InternalServerError,
                    ExceptionMessage.INVALID_TOKEN);
            }

            return jwtToken;
        }
    }
}
