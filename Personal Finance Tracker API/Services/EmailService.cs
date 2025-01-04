using Personal_Finance_Tracker_API.Models;
using System.Net;
using System.Net.Mail;

namespace Personal_Finance_Tracker_API.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailModel email);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(EmailModel email)
        {
            var email_ = configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var password = configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var host = configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            var port = configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");
            var smtpClient = new SmtpClient(host, port);

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(email_, password);

            var message = new MailMessage(email_!, email.Receptor, email.Subject, email.Body)
            {
                IsBodyHtml = true
            };
            await smtpClient.SendMailAsync(message);
        }
    }
}
