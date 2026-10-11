using JobApplicationTracker.Application.DTOs;
using JobApplicationTracker.Application.Interface;
using JobApplicationTracker.Domain.Entities;
using JobApplicationTracker.Domain.Repository;

namespace JobApplicationTracker.Application.Service
{
    public class ApplicationService : IApplicationService
    {
        public readonly IApplicationRepository _repository;
        public ApplicationService(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationResponse> AddApplicationAsync(AddApplicationRequest request)
        {
            var applicationn = new JobApplicationTracker.Domain.Entities.Application
            {
                CompanyName = request.CompanyName,
                Position = request.Position,
                AppliedDate = request.AppliedDate,
                Status = ApplicationStatus.Applied
            };

            var saved = await _repository.AddAsync(applicationn);

            return new ApplicationResponse
            {
                Id = saved.Id,
                CompanyName = saved.CompanyName,
                Position = saved.Position,
                AppliedDate = saved.AppliedDate,
                Status = saved.Status
            };
            
        }

        public async Task<List<ApplicationResponse>> GetAllApplicationsAsync()
        {
            var applications = await _repository.GetAllAsync();

            return  applications.Select(a => new ApplicationResponse
            {
                Id = a.Id,
                CompanyName = a.CompanyName,
                Position = a.Position,
                AppliedDate = a.AppliedDate,
                Status = a.Status
            }).ToList();
        }

        public async Task<List<ApplicationResponse>> GetPendingFollowUpsAsync()
        {
            var applications = await _repository.GetPendingFollowUpsAsync(7);
            return applications.Select(a => new ApplicationResponse
            {
                Id = a.Id,
                CompanyName = a.CompanyName,
                Position = a.Position,
                AppliedDate = a.AppliedDate,
                Status = a.Status
            }).ToList();
        }

        public async Task<bool> UpdateStatusAsync(int id, ApplicationStatus status)
        {
            var application = await _repository.GetByIdAsync(id);

            if (application == null)
                return false;
            application.Status = status;
            await _repository.UpdateAsync(application);
            return true;
        }


    }
}
