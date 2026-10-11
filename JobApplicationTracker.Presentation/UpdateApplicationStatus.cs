using JobApplicationTracker.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using JobApplicationTracker.Application.DTOs;


namespace JobApplicationTracker.Presentation;

public class UpdateApplicationStatus
{
    private readonly ILogger<UpdateApplicationStatus> _logger;
    private readonly IApplicationService _applicationService;

    public UpdateApplicationStatus(ILogger<UpdateApplicationStatus> logger , IApplicationService applicationService)
    {
        _logger = logger;
        _applicationService = applicationService;
    }

    [Function("UpdateApplicationStatus")]
    public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "put" , Route = "applications/{id}")] HttpRequest req , int id)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonSerializer.Deserialize<UpdateStatusRequest>(requestBody,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (request == null)
        {
            return new BadRequestObjectResult("Invalid request body.");
        }

        var success = await _applicationService.UpdateStatusAsync(id, request.Status);
        if (!success)
        {
            return new NotFoundObjectResult($"Application with id {id} not found.");
        }

        return new OkObjectResult("Status updated successfully.");
    }
}