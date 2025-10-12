using System.ComponentModel.DataAnnotations.Schema;

namespace AutoML.Data.Models
{
    [Table("Tenants")]
    public class TenantEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public List<TenantUserEntity> TenantUsers { get; set; } = new();
    }
}
