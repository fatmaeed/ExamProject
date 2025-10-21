using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace ExamProject.Domain.Expections
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {


            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var client = new SmtpClient(emailSettings.Host, emailSettings.Port)
            {
                Credentials = new NetworkCredential(emailSettings.FromEmail, emailSettings.Password),
                EnableSsl = true,
                UseDefaultCredentials = false

            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.FromEmail, emailSettings.FromName),
                Subject = subject,
                Body = htmlMessage,
                To = { new MailAddress(email) },
                IsBodyHtml = true
            };

            await client.SendMailAsync(mailMessage);


        }
    }
}