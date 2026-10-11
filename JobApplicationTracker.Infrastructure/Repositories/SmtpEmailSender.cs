using JobApplicationTracker.Application.DTOs;
using JobApplicationTracker.Application.Interface;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;

namespace JobApplicationTracker.Infrastructure.Repositories
{
    public class SmtpEmailSender : IEmailSender

    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;
        private readonly string _senderEmail;
        public SmtpEmailSender(IConfiguration configuration)
        {
            _host = configuration["SmtpHost"]!;
            _port = int.Parse(configuration["SmtpPort"]!);
            _user = configuration["SmtpUser"]!;
            _password = configuration["SmtpPassword"]!;
            _senderEmail = configuration["SenderEmail"]!;
        }
 
        public async Task SmtpEmailSenderAsync(SendEmailRequest sendEmail)
        {
            using var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_user, _password),
                EnableSsl = true
            };
            using var message = new MailMessage(_senderEmail, sendEmail.to, sendEmail.subject, sendEmail.body);

            await client.SendMailAsync(message);
        }
    }
}
