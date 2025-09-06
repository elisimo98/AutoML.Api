using System.ComponentModel.DataAnnotations;

namespace AutoML.Web.Models
{
    public class CreateProjectViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
