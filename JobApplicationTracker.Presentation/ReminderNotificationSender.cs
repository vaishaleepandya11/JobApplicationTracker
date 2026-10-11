using Azure.Storage.Queues.Models;
using EllipticCurve.Utils;
using JobApplicationTracker.Application.DTOs;
using JobApplicationTracker.Application.Interface;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;

namespace JobApplicationTracker.Presentation;

public class ReminderNotificationSender
{
    private readonly ILogger<ReminderNotificationSender> _logger;
    private readonly IEmailSender _emailSender;
    public ReminderNotificationSender(ILogger<ReminderNotificationSender> logger,IEmailSender emailSender)
    {
        _logger = logger;
        _emailSender = emailSender;
    }

    [Function(nameof(ReminderNotificationSender))]
    public async Task Run([QueueTrigger("reminder-queue", Connection = "")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);

        await _emailSender.SmtpEmailSenderAsync(new SendEmailRequest
        {
            to = "vaishalipandya1198@gmail.com",
            subject = "Follow-up Reminder",
            body = message.MessageText
        });
        _logger.LogInformation("Notification sent (simulated) at: {time}", DateTime.Now);
    }
}