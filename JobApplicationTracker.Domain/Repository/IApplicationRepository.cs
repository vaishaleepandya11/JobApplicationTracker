using JobApplicationTracker.Domain.Entities;

namespace JobApplicationTracker.Domain.Repository
{
    public interface IApplicationRepository
    {
        Task<Application> AddAsync(Application application);
        Task<List<Application>> GetAllAsync();
        Task<Application?> GetByIdAsync(int id);
        Task UpdateAsync(Application application);
        Task<List<Application>> GetPendingFollowUpsAsync(int daysThreshold);

    }
}
