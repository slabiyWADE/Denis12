using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Models
{
    public class Job
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Specialty { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        // FK to Employer
        [Required]
        public Guid EmployerId { get; set; }

        [ForeignKey(nameof(EmployerId))]
        public Employer Employer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
