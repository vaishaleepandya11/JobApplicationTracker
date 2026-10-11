using JobApplicationTracker.Application.DTOs;
using JobApplicationTracker.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace JobApplicationTracker.Presentation;

public class AddApplication
{
    private readonly ILogger<AddApplication> _logger;
    public readonly IApplicationService _applicationService;
    public AddApplication(ILogger<AddApplication> logger , IApplicationService applicationService)
    {
        _logger = logger;
        _applicationService = applicationService;
    }

    [Function("AddApplication")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("Add Application request received.");
        string requestBody= await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonSerializer.Deserialize<AddApplicationRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (request == null)
        {
            return new BadRequestObjectResult("Invalid request body.");
        }
        var result = await _applicationService.AddApplicationAsync(request);
        _logger.LogInformation("New Application Added.");
        return new CreatedResult($"/api/applications/{result.Id}", result);

    }
}