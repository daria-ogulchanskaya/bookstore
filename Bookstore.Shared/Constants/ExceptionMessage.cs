namespace Bookstore.Shared.Constants
{
    public static partial class Constants
    {
        public static partial class AppSettings
        {
            public static class ExceptionMessage
            {
                public const string EMPTY_FIELD = "The field is empty";
                public const string USER_NOT_FOUND = "User is not found";
                public const string USER_IS_BLOCKED = "User is blocked";
                public const string INVALID_CREDENTIALS = "Invalid password or email";
                public const string CREATE_USER_FAILED = "User creation operation failed";
                public const string REFRESH_TOKEN_NOT_EQUALS = "Tokens does not match";
                public const string EMAIL_ALREADY_EXISTS = "Email already exists";
                public const string USERNAME_ALREADY_EXISTS = "Username already exists";

                public const string EMPTY_AUTHOR = "Data about author is empty";
                public const string AUTHOR_NOT_FOUND = "Author not found";
                public const string AUTHOR_ALREADY_EXISTS = "Author already exists";

                public const string EMPTY_PRINTING_EDITION = "Printing edition is empty";
                public const string PRINTING_EDITION_NOT_FOUND = "Printing edition is not found";
                public const string PRINTING_EDITION_ALREADY_EXISTS = "Printing edition already exists";

                public const string INVALID_TOKEN = "Invalid token";
                public const string ID_DOES_NOT_EXISTS = "Id does not exists";
                public const string INVALID_ID = "Invalid Id";

                public const string PAYMENT_FAILED = "Order payment error";

                public const string EMPTY_ORDER = "Order is empty";
                public const string ORDER_NOT_FOUND = "Order not found";
                public const string ORDER_ALREADY_EXISTS = "Order already exists";

                public const string FILTER_FAILED = "Filter failed";
                public const string CONVERT_FAILED = "Convert failed";

                public const string INVALID_DATA = "The provided data is not valid";
            }
        }
    }
}
