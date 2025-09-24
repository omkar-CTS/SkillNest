using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.DTO;
using SkillNest.Models;
using System.Security.Claims;

namespace SkillNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication
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
            // Extract user id and role from JWT claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null)
                return Unauthorized("Invalid token.");

            var userId = int.Parse(userIdClaim.Value);
            var role = roleClaim.Value;

            // Employees can only view their own profile
            if (role == "Employee" && userId != id)
                return Forbid("Employees can only view their own profile.");

            // Managers can view anyone's profile (no additional check needed)

            var employee = await _context.Employees
                .Include(e => e.Skills)
                .Include(e => e.Projects)
                .Include(e => e.Certificates)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound("Employee not found!");

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

        // (Optional) For managers: get all employees
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Skills)
                .Include(e => e.Projects)
                .Include(e => e.Certificates)
                .ToListAsync();

            var response = employees.Select(employee => new EmployeeResponseDTO
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
            });

            return Ok(response);
        }
    }
}