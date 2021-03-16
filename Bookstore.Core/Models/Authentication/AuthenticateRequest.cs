using System.ComponentModel.DataAnnotations;

namespace Bookstore.Core.Models.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
