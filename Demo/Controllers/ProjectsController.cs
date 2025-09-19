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
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetProjectsByEmployee(int employeeId)
        {
            var projects = await _context.Projects
                .Where(p => p.EmployeeId == employeeId)
                .ToListAsync();

            if (!projects.Any())
            {
                return Ok(new { Message = "No projects added yet. Please add a project." });
            }

            var projectDTOs = projects.Select(p => new ProjectResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            });
            return Ok(projectDTOs);
        }

        [HttpPost("employee/{employeeId}")]
        public async Task<IActionResult> AddProject(int employeeId, [FromBody] ProjectAddDTO projectAddDTO)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
                return NotFound("Employee not found!");

            var project = new Projects
            {
                Name = projectAddDTO.ProjectName,
                Description = projectAddDTO.Description,
                StartDate = projectAddDTO.StartDate,
                EndDate = projectAddDTO.EndDate,
                EmployeeId = employeeId
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Project Added Successfully.", ProjectId = project.Id });
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, UpdateProjectDTO updateProjectDTO)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
                return NotFound("Project not found!");

            project.Name = updateProjectDTO.ProjectName;
            project.Description = updateProjectDTO.Description;
            project.StartDate = updateProjectDTO.StartDate;
            project.EndDate = updateProjectDTO.EndDate;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Project Updated Successfully." });
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
                return NotFound("Project not found!");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Project Deleted Successfully." });
        }
    }
}
