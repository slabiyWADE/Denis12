using System.ComponentModel.DataAnnotations;

namespace BaseApi.DTOs
{
    public class RegisterUserDto
    {
        [Required] public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required] public string Phone { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, MinLength(6)] public string Password { get; set; }
    }
}
