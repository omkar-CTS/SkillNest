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
    public class ResumesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ResumesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private string GetWebRootPath()
        {
            return _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        [HttpPost("{employeeId}")]
        public async Task<IActionResult> UploadResume(int employeeId, [FromForm] ResumeUploadDTO resumeUploadDTO)
        {
            var employee = await _context.Employees
                .Include(e => e.Resume)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound("Employee not found!");

            if (resumeUploadDTO.File == null || resumeUploadDTO.File.Length == 0)
                return BadRequest("No file uploaded.");

            if (Path.GetExtension(resumeUploadDTO.File.FileName).ToLower() != ".pdf")
                return BadRequest("Only PDF files are allowed.");

            var uploadsFolder = Path.Combine(GetWebRootPath(), "upload", "resumes");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + resumeUploadDTO.File.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await resumeUploadDTO.File.CopyToAsync(stream);
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
                var oldFilePath = Path.Combine(GetWebRootPath(), employee.Resume.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
                employee.Resume.FilePath = "/uploads/resumes/" + uniqueFileName;
            }
            await _context.SaveChangesAsync();

            var response = new ResumeResponseDTO
            {
                Id = employee.Resume.Id,
                FilePath = employee.Resume.FilePath,
                EmployeeId = employee.Resume.EmployeeId
            };
            return Ok(response);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateResume(int employeeId, [FromForm] ResumeUploadDTO resumeUploadDTO)
        {
            var employee = await _context.Employees
                .Include(e => e.Resume)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound("Employee not found!");

            if (resumeUploadDTO.File == null || resumeUploadDTO.File.Length == 0)
                return BadRequest("No file uploaded.");

            if (Path.GetExtension(resumeUploadDTO.File.FileName).ToLower() != ".pdf")
                return BadRequest("Only PDF files are allowed.");

            var uploadsFolder = Path.Combine(GetWebRootPath(), "upload", "resumes");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + resumeUploadDTO.File.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await resumeUploadDTO.File.CopyToAsync(stream);
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
                var oldFilePath = Path.Combine(GetWebRootPath(), employee.Resume.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
                employee.Resume.FilePath = "/uploads/resumes/" + uniqueFileName;
            }

            await _context.SaveChangesAsync();

            var response = new ResumeResponseDTO
            {
                Id = employeeId,
                FilePath = employee.Resume.FilePath,
                EmployeeId = employee.Resume.EmployeeId
            };

            return Ok(response);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> DownloadResume(int employeeId)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == employeeId);

            if (resume == null || string.IsNullOrEmpty(resume.FilePath))
                return NotFound("Resume Not Found!");

            var filePath = Path.Combine(GetWebRootPath(), resume.FilePath.TrimStart('/'));
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
