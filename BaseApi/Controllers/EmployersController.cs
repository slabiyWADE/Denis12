using BaseApi.Data;
using BaseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployersController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Регистрация работодателя
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Employer request)
        {
            if (await _context.Employers.AnyAsync(e => e.Email == request.Email))
                return BadRequest(new { message = "Работодатель с таким Email уже существует" });

            // Создаём нового работодателя
            var employer = new Employer
            {
                Id = Guid.NewGuid(),
                CompanyName = request.CompanyName,
                ContactPerson = request.ContactPerson,
                Phone = request.Phone,
                Email = request.Email,
                PasswordHash = HashPassword(request.PasswordHash)
            };

            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Регистрация работодателя успешна", employer.Id });
        }

        // 🔹 Получить всех работодателей
        [HttpGet]
        public async Task<IActionResult> GetAllEmployers()
        {
            var employers = await _context.Employers
                .Include(e => e.Jobs)
                .ToListAsync();

            return Ok(employers);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
