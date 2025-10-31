using System.ComponentModel.DataAnnotations;

namespace BaseApi.DTOs
{
    public class RegisterEmployerDto
    {
        [Required] public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        [Required] public string Phone { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, MinLength(6)] public string Password { get; set; }
    }
}
