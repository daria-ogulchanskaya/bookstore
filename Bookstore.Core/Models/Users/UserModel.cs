namespace Bookstore.Core.Models.Users
{
    public class UserModel : Model
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsBlocked { get; set; }
    }
}
