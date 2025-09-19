using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.DTO;
using SkillNest.Models;


namespace SkillNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context) 
        { 
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Skills)
                .Include(e => e.Projects)
                .Include(e => e.Certificates)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound("Emloyee not found!");

            var EmployeeResponseDTO = new EmployeeResponseDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Location = employee.Location,
                Role = employee.Role,
                Skills = employee.Skills?.Select(s => new SkillResponseDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Proficiency = s.Proficiency
                }).ToList(),
                Projects = employee.Projects?.Select(p => new ProjectResponseDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                }).ToList(),
                Certificates = employee.Certificates?.Select(c => new CertificateResponseDTO
                {
                    Id = c.Id,
                    Title = c.Name,
                    CertificateNumber = c.CertificateNumber,
                    DateObtained = c.DateObtained
                }).ToList()
            };
            return Ok(EmployeeResponseDTO);
        }
    }
}
