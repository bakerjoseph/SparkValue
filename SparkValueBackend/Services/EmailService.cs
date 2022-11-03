using SendGrid;
using SendGrid.Helpers.Mail;
using SparkValueBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Services
{
    public class EmailService
    {
        private readonly string APIKey;
        private readonly string SendingEmail;

        public EmailService()
        {
            // API key is created here, in the future store in some other place secure
            APIKey = "";
            // Get our from address from some other secure palce
            SendingEmail = "learningelectronics@sparkvalue.com";
        }

        public async Task SendWelcomeEmail(UserAccountModel user)
        {
            SendGridClient client = new SendGridClient(APIKey);
            SendGridMessage msg = new SendGridMessage()
            {
                From = new EmailAddress(SendingEmail, "Spark Value Bot"),
                Subject = "Welcome to Spark Value",
                PlainTextContent = $"Welcome, {user.Username}, to the world of electronics! " +
                                    "We hope that you learn a lot and have fun while doing so.",
                HtmlContent = $"<p>Welcome, {user.Username}, to the world of electronics! " +
                               "We hope that you learn a lot and have fun while doing so. </p>"
            };
            msg.AddTo(user.EmailAddress, user.Username);
            Response response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async Task SendPasswordResetEmail(UserAccountModel user, string passwordReset)
        {
            SendGridClient client = new SendGridClient(APIKey);
            SendGridMessage msg = new SendGridMessage()
            {
                From = new EmailAddress(SendingEmail, "Spark Value Bot"),
                Subject = "Welcome to Spark Value",
                PlainTextContent = $"You, {user.Username}, have requested to change your password. " +
                                   "Please enter the following code in the application to continue this process. " +
                                   $"{passwordReset}",
                HtmlContent = $"<p>You, {user.Username}, have <strong>requested to change your password</strong>. " +
                               "Please enter the following code in the application to continue this process. </p>" +
                               $"<p><strong>{passwordReset}</strong></p>"
            };
            msg.AddTo(user.EmailAddress, user.Username);
            Response response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
