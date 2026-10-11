using JobApplicationTracker.Domain.Entities;
using JobApplicationTracker.Domain.Repository;
using JobApplicationTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Infrastructure.Repositories
{
    public class ApplicationRepository :IApplicationRepository
    {
        private readonly AppDbContext _context;

        public ApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Application> AddAsync(Domain.Entities.Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<List<Domain.Entities.Application>> GetAllAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Domain.Entities.Application?> GetByIdAsync(int id)
        {
            return await _context.Applications.FindAsync(id);
        }

        public async Task<List<Domain.Entities.Application>> GetPendingFollowUpsAsync(int daysThreshold)
        {
            var cutoffDate = DateTime.Now.AddDays(-daysThreshold);
            return await _context.Applications
               .Where(a => a.Status == ApplicationStatus.Applied && (a.LastFollowUpDate == null || a.AppliedDate <= cutoffDate))
               .ToListAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Application application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();
        }
    }
}
