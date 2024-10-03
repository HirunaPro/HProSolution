using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPro.Api.Data.Entities
{
    public class ApplicationTag
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string TagName { get; set; }

        [Required]
        [StringLength(50)]
        public string TagType { get; set; }

        public Guid? ParentTagId { get; set; }


        // Navigation properties
        public ApplicationTag ParentTag { get; set; }
        public ICollection<ApplicationTag> ChildTags { get; set; }
        public ICollection<ObjectTag> ObjectTags { get; set; }
    }
}
