using AutoML.Web.Data.Interfaces;
using AutoML.Web.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoML.Web.Data.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext context;

        public ProjectService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await context.Projects.ToListAsync();
        }

        public async Task<List<Project>> GetProjectByUserIdAsync(string userId)
        {
            var projects = await context.Projects
                .Where(p => p.Tenant.TenantUsers.Any(tu => tu.UserId == userId))
                .Include(p => p.Tenant)
                .ToListAsync();

            return projects;
        }

        public async Task<Project?> GetProjectByIdAsync(long projectId)
        {
            return await context.Projects
                .Include(p => p.Tenant)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task<Project> CreateProjectAsync(string name, string description, string userId)
        {
            var tenant = await context.TenantUsers
                .Where(tu => tu.UserId == userId)
                .Select(tu => tu.Tenant)
                .FirstOrDefaultAsync();

            if (tenant == null)
            {
                throw new Exception("Tenant not found for user.");
            }

            var project = new Project
            {
                Name = name,
                Description = description,
                TenantId = tenant.Id,
                CreatedAt = DateTime.UtcNow,
                OwnerUserId = userId
            };

            context.Projects.Add(project);
            await context.SaveChangesAsync();

            return project;
        }

        public async Task UpdateProjectAsync(Project project)
        {
            context.Projects.Update(project);
            await context.SaveChangesAsync();
        }
    }
}
