using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bookstore.Shared.Models.Settings
{
    public class AuthSettings {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string Secret { get; set; }

        public string TokenName { get; set; }

        public int TokenLifeTime { get; set; }

        public SymmetricSecurityKey GetKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    }
}