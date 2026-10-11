using JobApplicationTracker.Application.DTOs;

namespace JobApplicationTracker.Application.Interface
{
    public interface IEmailSender
    {
        Task SmtpEmailSenderAsync(SendEmailRequest sendEmail);
    }
}
