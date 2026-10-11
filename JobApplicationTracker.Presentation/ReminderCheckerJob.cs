using System;
using JobApplicationTracker.Application.Interface;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace JobApplicationTracker.Presentation;

public class ReminderCheckerJob
{
    private readonly ILogger _logger;
    private readonly IApplicationService _applicationService;
    public ReminderCheckerJob(ILoggerFactory loggerFactory , IApplicationService applicationService)
    {
        _logger = loggerFactory.CreateLogger<ReminderCheckerJob>();
        _applicationService = applicationService;
    }

    [Function("ReminderCheckerJob")]
    [QueueOutput("reminder-queue")]
    public async Task<string[]> Run([TimerTrigger("0 0 9 * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);

        var pendingApplications = await _applicationService.GetPendingFollowUpsAsync();

        _logger.LogInformation("Found {count} applications needing follow-up.", pendingApplications.Count);

        foreach (var app in pendingApplications)
        {
            _logger.LogInformation("Reminder: Follow up on {company} - applied on {date}",
                app.CompanyName, app.AppliedDate);
        }

        var messages = pendingApplications.Select(app => $"Follow up needed: {app.CompanyName} - {app.Position} (Applied: {app.AppliedDate:yyyy-MM-dd})").ToArray();
        return messages;

    }
}