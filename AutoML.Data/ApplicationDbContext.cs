using AutoML.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoML.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<TenantEntity> Tenants { get; set; } = null!;
        public DbSet<TenantUserEntity> TenantUsers { get; set; } = null!;
        public DbSet<ModelConfigEntity> ModelConfigs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ModelConfig → Tenant (uses external ID)
            builder.Entity<ModelConfigEntity>(mc =>
            {
                mc.HasKey(x => x.Id);

                mc.HasOne(x => x.Tenant)
                  .WithMany()
                  .HasForeignKey(x => x.TenantId)
                  .HasPrincipalKey(t => t.ExternalId);
            });

            // TenantUser → Tenant (uses internal ID)
            builder.Entity<TenantUserEntity>(tu =>
            {
                tu.HasKey(x => new { x.Id, x.UserId });

                tu.HasOne(x => x.Tenant)
                  .WithMany(t => t.TenantUsers)
                  .HasForeignKey(x => x.TenantId)
                  .HasPrincipalKey(t => t.Id);

                tu.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);
            });

            builder.Entity<TenantEntity>()
                .HasIndex(t => t.ExternalId)
                .IsUnique();
        }
    }

}
