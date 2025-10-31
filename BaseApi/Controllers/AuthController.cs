using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseApi.Data;
using BaseApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // Регистрация
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest(new { message = "Пользователь с таким email уже существует" });

            var user = new User
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                LastName = request.LastName,
                Phone = request.Phone
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Регистрация успешна" });
        }

        // Авторизация
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || user.PasswordHash != HashPassword(request.Password))
                return Unauthorized(new { message = "Неверный логин или пароль" });

            // Временно без JWT — добавим потом
            return Ok(new { message = "Вход выполнен успешно" });
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

    // DTO-модель для входа и регистрации
    public class RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
