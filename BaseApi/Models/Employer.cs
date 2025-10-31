using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Models
{
    public class Employer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string CompanyName { get; set; }

        [MaxLength(200)]
        public string ContactPerson { get; set; }

        [Required, MaxLength(100)]
        public string Phone { get; set; }

        [Required, MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Job> Jobs { get; set; }
    }
}
