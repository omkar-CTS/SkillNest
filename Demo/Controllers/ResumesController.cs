using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.Models;

namespace SkillNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ResumesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("{employeeId}")]
        public async Task<IActionResult> UploadResume(int employeeId, IFormFile file)
        {
            var employee = await _context.Employees
                .Include(e => e.Resume)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound("Employee not found!");

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "upload/resumes");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            if (employee.Resume == null)
            {
                employee.Resume = new Resume
                {
                   FilePath = "/uploads/resumes/" + uniqueFileName,
                   EmployeeId = employeeId,
                };
                _context.Resumes.Add(employee.Resume);
            }
            else
            {
                var oldFilePath = Path.Combine(_environment.WebRootPath, employee.Resume.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
                employee.Resume.FilePath = "/uploads/resumes/" + uniqueFileName;
            }
            await _context.SaveChangesAsync();
            return Ok(employee.Resume);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> DownloadResume(int employeeId)
        {
            var resume = await _context.Resumes
                .FirstOrDefaultAsync(r => r.EmployeeId == employeeId);

            if (resume == null || string.IsNullOrEmpty(resume.FilePath))
                return NotFound("Resume not found!");

            var filePath = Path.Combine(_environment.WebRootPath, resume.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found on server!");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/pdf", "resume.pdf");
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteResume(int employeeId)
        {
            var resume = await _context.Resumes
                .FirstOrDefaultAsync(r => r.EmployeeId == employeeId);

            if (resume == null)
                return NotFound("Resume not found!");

            var filePath = Path.Combine(_environment.WebRootPath, resume.FilePath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();

            return Ok("Resume deleted successfully.");
        }
    }
}
