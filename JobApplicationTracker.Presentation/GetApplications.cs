using JobApplicationTracker.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace JobApplicationTracker.Presentation;

public class GetApplications
{
    private readonly ILogger<GetApplications> _logger;
    private readonly IApplicationService _applicationaervice;
    public GetApplications(ILogger<GetApplications> logger , IApplicationService applicationService)
    {
        _logger = logger;
        _applicationaervice = applicationService;
    }

    [Function("GetApplications")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "applications")] HttpRequest req)
    {
        _logger.LogInformation("C# Get Applications request received.");

        var result = await _applicationaervice.GetAllApplicationsAsync();

        return new OkObjectResult(result);
    }
}