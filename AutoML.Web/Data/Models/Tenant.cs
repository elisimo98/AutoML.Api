using AutoML.Web.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoML.Web.Data.Models
{
    [Table("Tenants")]
    public class Tenant
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Project> Projects { get; set; } = new();
        public List<TenantUser> TenantUsers { get; set; } = new();
    }
}
