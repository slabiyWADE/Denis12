using System.ComponentModel.DataAnnotations;

namespace BaseApi.DTOs
{
    public class CreateJobDto
    {
        [Required] public string Title { get; set; }
        public string Specialty { get; set; }
        public string Description { get; set; }

        // EmployerId — передаётся из клиента (авторизация не реализована минимально)
        [Required] public Guid EmployerId { get; set; }
    }
}
