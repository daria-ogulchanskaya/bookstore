using Bookstore.Shared.Models.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static Bookstore.Shared.Constants.Constants.AppSettings;

namespace Bookstore.Core.Providers
{
    public class EmailProvider
    {
        private readonly MailAddress _from;
        private readonly SmtpClient _smtp;

        public EmailProvider(IOptions<SmtpSettings> smtpSettings)
        {
            var smtp = smtpSettings.Value;
            
            var credentials = new NetworkCredential(
                smtp.Email,
                smtp.Password);

            _from = new MailAddress(smtp.Email);
            _smtp = new SmtpClient
            {
                Credentials = credentials,
                Host = smtp.Host,
                EnableSsl = true
            };
        }

        public async Task SendEmailConfirmationMessageAsync(string email, string url)
        {
            var to = new MailAddress(email);

            using var message = new MailMessage(_from, to)
            {
                Subject = EmailConfirmation.SUBJECT,
                Body = $"{EmailConfirmation.MESSAGE} {url}"
            };

            await _smtp.SendMailAsync(message);
        }

        public async Task SendPasswordResetMessageAsync(string email , string password)
        {
            var to = new MailAddress(email);

            using var message = new MailMessage(_from, to)
            {
                Subject = PasswordReset.SUBJECT,
                Body = $"{PasswordReset.MESSAGE} {password}"
            };

            await _smtp.SendMailAsync(message);
        }
    }
}
