using JobApplicationTracker.Application.Interface;
using JobApplicationTracker.Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace JobApplicationTracker.Presentation;

public class UploadResume
{
    private readonly ILogger<UploadResume> _logger;
    private readonly AttachResumeService _attachResume;

    public UploadResume(ILogger<UploadResume> logger, AttachResumeService attachResume)
    {
        _logger = logger;
        _attachResume = attachResume;
    }

    [Function("UploadResume")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post" , Route = "resumes")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var form = await req.ReadFormAsync();
        var file = form.Files.FirstOrDefault();

        if (file == null || file.Length == 0)
            return new BadRequestObjectResult("File missing.");

        if (!int.TryParse(form["applicationId"], out var applicationId))
            return new BadRequestObjectResult("Valid applicationId required.");

        using var stream = file.OpenReadStream();
        var blobName = await _attachResume.ExecuteAsync(
        applicationId, file.FileName, stream, file.ContentType);

        if (blobName is null)
            return new NotFoundObjectResult($"Application {applicationId} not found.");

        return new OkObjectResult(new { applicationId, blobName });

    }
}