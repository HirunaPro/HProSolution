using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPro.Api.Data.Entities
{
    public class ObjectTag
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ObjectId { get; set; }

        // public ObjectType ObjectType { get; set; }

        [Required]
        [StringLength(50)]
        public string ObjectType { get; set; }

  
        public Guid ApplicationTagId { get; set; }

        // Navigation property
        public ApplicationTag ApplicationTag { get; set; }

    }
    
}
