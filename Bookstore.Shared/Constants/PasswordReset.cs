namespace Bookstore.Shared.Constants
{
    public static partial class Constants
    {
        public static partial class AppSettings
        {
            public static class PasswordReset
            {
                public const string SUBJECT = "Password Reset";
                public const string MESSAGE = "Your temporary password: ";
                public const int SIZE = 20;
            }
        }
    }
}
