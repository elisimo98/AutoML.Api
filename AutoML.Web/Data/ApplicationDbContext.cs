using AutoML.Web.Data.Models;
using AutoML.Web.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoML.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Tenant> Tenant { get; set; } = null!;
        public DbSet<TenantUser> TenantUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Project -> Team (many-to-one)
            builder.Entity<Project>()
                .HasOne(p => p.Tenant)
                .WithMany(t => t.Projects)
                .HasForeignKey(p => p.TenantId);

            // TeamUser composite key + relationship
            builder.Entity<TenantUser>()
                .HasKey(tu => new { tu.TenantId, tu.UserId });

            builder.Entity<TenantUser>()
                .HasOne(tu => tu.Tenant)
                .WithMany(t => t.TenantUsers)
                .HasForeignKey(tu => tu.TenantId);

            builder.Entity<TenantUser>()
            .HasOne(tu => tu.User)
            .WithMany()
            .HasForeignKey(tu => tu.UserId);
        }
    }
}
