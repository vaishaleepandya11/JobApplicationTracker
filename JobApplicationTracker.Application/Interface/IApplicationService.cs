using JobApplicationTracker.Application.DTOs;
using JobApplicationTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JobApplicationTracker.Application.Interface
{
    public interface IApplicationService
    {
        Task<ApplicationResponse> AddApplicationAsync(AddApplicationRequest request);
        Task<List<ApplicationResponse>> GetAllApplicationsAsync();
        Task<bool> UpdateStatusAsync(int id, ApplicationStatus status);

        Task<List<ApplicationResponse>> GetPendingFollowUpsAsync();
    }
}
