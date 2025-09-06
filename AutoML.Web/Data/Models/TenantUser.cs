using System.ComponentModel.DataAnnotations.Schema;

namespace AutoML.Web.Data.Models
{
    [Table("TenantUsers")]
    public class TenantUser
    {
        public long TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public string Role { get; set; } = "Member";

    }
}
