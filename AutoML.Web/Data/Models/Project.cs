using AutoML.Web.Data.Models;

namespace AutoML.Web.Models.Data
{
	public class Project
	{
		public long Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string Status { get; set; } = "Not Started";
		public string? FileName { get; set; }
		public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string? OwnerUserId { get; set; }
        public long TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
