using System.ComponentModel.DataAnnotations.Schema;

namespace AutoML.Data.Models
{
    [Table("TenantUsers")]
    public class TenantUserEntity
    {
        public long Id { get; set; }
        public long TenantId { get; set; } 
        public TenantEntity Tenant { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public string Role { get; set; } = "Member";

    }
}
