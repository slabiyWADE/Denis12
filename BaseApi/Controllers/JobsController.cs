using BaseApi.Data;
using BaseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Создание вакансии
        [HttpPost("create")]
        public async Task<IActionResult> CreateJob([FromBody] Job request)
        {
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.Id == request.EmployerId);
            if (employer == null)
                return BadRequest(new { message = "Работодатель с таким ID не найден" });

            var job = new Job
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Specialty = request.Specialty,
                Description = request.Description,
                EmployerId = employer.Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Вакансия успешно добавлена", job.Id });
        }

        // 🔹 Получить все вакансии
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _context.Jobs
                .Include(j => j.Employer)
                .OrderByDescending(j => j.CreatedAt)
                .ToListAsync();

            return Ok(jobs);
        }

        // 🔹 Получить вакансии конкретного работодателя
        [HttpGet("by-employer/{employerId:guid}")]
        public async Task<IActionResult> GetJobsByEmployer(Guid employerId)
        {
            var jobs = await _context.Jobs
                .Where(j => j.EmployerId == employerId)
                .Include(j => j.Employer)
                .ToListAsync();

            return Ok(jobs);
        }
    }
}
