using Microsoft.EntityFrameworkCore;
using JobApplicationTracker.Domain.Entities;


namespace JobApplicationTracker.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
       public DbSet<JobApplicationTracker.Domain.Entities.Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobApplicationTracker.Domain.Entities.Application>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.CompanyName).IsRequired().HasMaxLength(200);
                entity.Property(a => a.Position).IsRequired().HasMaxLength(200);
                entity.Property(a => a.Status).HasConversion<string>();
            });
        }
    }
}
