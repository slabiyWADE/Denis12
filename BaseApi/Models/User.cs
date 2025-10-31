using System.ComponentModel.DataAnnotations;

namespace BaseApi.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, MaxLength(100)]
        public string Phone { get; set; }

        [Required, MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Доп. поля:
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}