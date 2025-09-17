using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.DTO;
using SkillNest.Interfaces;
using SkillNest.Models;

namespace SkillNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(AppDbContext context, ITokenRepository tokenRepository)
        {
            _context = context;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == loginDTO.Email && e.Password == loginDTO.Password);

            if (employee == null || employee.Password != loginDTO.Password)
                return Unauthorized("Invalid email or password.");

            var token = _tokenRepository.CreateToken(employee.Id, employee.Email, employee.Role);
            return Ok(new
            {
                Token = token,
                Role = employee.Role,
                Name = employee.Name,
            });
        }
    }
}
