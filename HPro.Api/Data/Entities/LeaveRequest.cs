using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPro.Api.Data.Entities
{
    public class LeaveRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

       
        public int UserId { get; set; }

        [Required]
        public DateTime StartDate  { get; set; }
        [Required]
        public DateTime EndDate  { get; set; }


        // Navigation properties
        public User User { get; set; }
        public ICollection<ObjectTag> Tags { get; set; }
    }
}
