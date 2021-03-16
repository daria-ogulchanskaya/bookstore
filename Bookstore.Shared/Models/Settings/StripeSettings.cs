namespace Bookstore.Shared.Models.Settings
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }

        public string SucceededChargeStatus { get; set; }

        public string Currency { get; set; }
    }
}
