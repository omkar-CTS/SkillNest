using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.Models;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SkillsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("employeeId")]
        public async Task<IActionResult> GetSkills(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Skills)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound("Employee not found!");

            return Ok(employee.Skills);
        }

        [HttpPost("employeeId")]
        public async Task<IActionResult> AddSkills(int employeeId, [FromBody] Skill skill)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
                return NotFound("Employee not found!");

            skill.EmployeeId = employeeId;
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return Ok(skill);
        }

        [HttpPut("{employeeId}/{skillId}")]
        public async Task<IActionResult> UpdateSkills(int employeeId, int skillId, [FromBody] Skill updatedSkill)
        {
            var skill = await _context.Skills
                .FirstOrDefaultAsync(s => s.Id == skillId && s.EmployeeId == employeeId);

            if (skill == null)
                return NotFound("Skill not found for this employee!");

            skill.Name = updatedSkill.Name;
            skill.Proficiency = updatedSkill.Proficiency;
            await _context.SaveChangesAsync();

            return Ok(skill);
        }

        [HttpDelete("{employeeId}/{skillId}")]
        public async Task<IActionResult> DeleteSkill(int employeeId, int skillId)
        {
            var skill = await _context.Skills
                .FirstOrDefaultAsync(s => s.Id == skillId && s.EmployeeId == employeeId);

            if (skill == null)
                return NotFound("Skill not found for this employee!");

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return Ok("Skill deleted successfully.");
        }
    }
}
