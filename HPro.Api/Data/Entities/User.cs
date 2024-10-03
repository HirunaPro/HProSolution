using System.ComponentModel.DataAnnotations;

namespace HPro.Api.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Eamil { get; set; } = string.Empty;


        // Navigation properties
        public ICollection<ObjectTag> Tags { get; set; }
    }
}
