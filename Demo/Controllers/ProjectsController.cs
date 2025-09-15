using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.Models;

namespace SkillNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetProjects(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Projects)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound("Employee not found!");

            return Ok(employee.Projects);
        }

        [HttpPost("{employeeId}")]
        public async Task<IActionResult> AddProject(int employeeId, [FromBody] Projects project)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
                return NotFound("Employee not found!");

            project.EmployeeId = employeeId;
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        [HttpPut("{employeeId}/{projectId}")]
        public async Task<IActionResult> UpdateProject(int employeeId, int projectId, [FromBody] Projects updatedProject)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId && p.EmployeeId == employeeId);

            if (project == null)
                return NotFound("Project not found for this employee!");

            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;
            project.StartDate = updatedProject.StartDate;
            project.EndDate = updatedProject.EndDate;

            await _context.SaveChangesAsync();

            return Ok(project);
        }
    }
}
