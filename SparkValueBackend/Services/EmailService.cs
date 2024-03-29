﻿using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using SparkValueBackend.Models;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SparkValueBackend.Services
{
    public class EmailService
    {
        private readonly string APIKey;
        private readonly string SendingEmail;

        public EmailService(EmailStatusStore emailStatusStore)
        {
            var config = new ConfigurationBuilder().AddUserSecrets<EmailService>().Build();

            APIKey = config["Email:ApiKey"];

            SendingEmail = config["Email:SendingEmail"];

            // Do we have the credentials to access the API?
            if (APIKey == null || SendingEmail == null) emailStatusStore.Status = false; 
            else emailStatusStore.Status = true;
        }

        /// <summary>
        /// Send a welcome email to the user.
        /// </summary>
        /// <param name="user">User to send the email to</param>
        /// <returns></returns>
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

        /// <summary>
        /// Send a password reset email to the user.
        /// </summary>
        /// <param name="user">User to send the email to</param>
        /// <param name="passwordReset">Password reset verification string</param>
        /// <returns></returns>
        public async Task SendPasswordResetEmail(UserAccountModel user, string passwordReset)
        {
            SendGridClient client = new SendGridClient(APIKey);
            SendGridMessage msg = new SendGridMessage()
            {
                From = new EmailAddress(SendingEmail, "Spark Value Bot"),
                Subject = "Password Reset Confirmation",
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
