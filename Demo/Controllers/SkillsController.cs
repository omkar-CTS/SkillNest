using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.DTO;
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

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetSkills(int employeeId)
        {
            var skills = await _context.Skills
                .Where(s => s.EmployeeId == employeeId)
                .Select(s => new SkillResponseDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Proficiency = s.Proficiency
                })
                .ToListAsync();

            if (skills == null || skills.Count == 0)
            {
                return NotFound("No skills found for this employee.");
            }

            return Ok(skills);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddSkills([FromBody] AddSkillListDTO addSkillDTO)
        {
            var employee = await _context.Employees.AnyAsync(e => e.Id == addSkillDTO.EmployeeId);

            if (employee == null)
            {
                return NotFound("Employee not found!");
            }    

            var skills = addSkillDTO.Skills.Select(s => new Skill
            {
                Name = s.Skills,
                Proficiency = s.Proficiency,
                EmployeeId = addSkillDTO.EmployeeId
            }).ToList();
            
            _context.Skills.AddRangeAsync(skills);
            await _context.SaveChangesAsync();

            var response = skills.Select(s => new SkillResponseDTO
            {
                Id = s.Id,
                Name = s.Name,
                Proficiency = s.Proficiency
            }).ToList();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] UpdateSkillDTO updateSkillDTO)
        {
            if (id != updateSkillDTO.Id)
                return BadRequest("Skill ID mismatch!");

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return NotFound("Skill not found!");

            skill.Name = updateSkillDTO.Skills;
            skill.Proficiency = updateSkillDTO.Proficiency;

            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            var response = new SkillResponseDTO
            {
                Id = skill.Id,
                Name = skill.Name,
                Proficiency = skill.Proficiency
            };
            return Ok(response);

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
