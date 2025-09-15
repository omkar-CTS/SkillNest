using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
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
                .Include(e => e.Resume)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound("Emloyee not found!");

            return Ok(employee);
        }
    }
}
