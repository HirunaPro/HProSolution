using System.ComponentModel.DataAnnotations;

namespace HPro.Api.Data.Entities
{
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Navigation property
        public ICollection<ObjectTag> Tags { get; set; }
    }
}
