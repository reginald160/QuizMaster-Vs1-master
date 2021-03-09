using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration config;

        public EmailSender(IConfiguration config)
        {
            this.config = config;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(subject, htmlMessage, email);
        }
        private Task Execute(string subject, string message, string email)
        {
            var key = config.GetSection("SendGrid").GetSection("SendGridKey").Value;
            var client = new SendGridClient(key);
            var from = new EmailAddress("ozougwu2016@gmail.com", "Quiz Master");
            var to = new EmailAddress(email, "End User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            var status = client.SendEmailAsync(msg);
            if (status.IsCompletedSuccessfully)
                return client.SendEmailAsync(msg);
            return client.SendEmailAsync(msg);
        }
    }
}
