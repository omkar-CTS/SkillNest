using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.Models;
using System.Runtime.CompilerServices;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public CertificatesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetCertificates(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Certificates)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
                return NotFound("Employee not found!");

            return Ok(employee.Certificates);
        }

        [HttpPost("{employeeId}")]
        public async Task<IActionResult> UploadCertificate(int employeeId, IFormFile file, [FromForm] string name, [FromForm] DateOnly date, [FromForm] string certificateNumber)
        {
            var employee = await _context.Employees.FindAsync(employeeId);

            if (employee == null)
                return NotFound("Employee not found!");
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var certificate = new Certificates
            {
                Name = name,
                DateObtained = date,
                CertificateNumber = certificateNumber,
                CertificateFilePath = "/uploads/" + uniqueFileName,
                EmployeeId = employeeId
            };
            
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();

            return Ok(certificate);
        }


        [HttpDelete("{employeeId}/{certificateId}")]
        public async Task<IActionResult> DeleteCertificate(int employeeId, int certificateId)
        {
            var certificate = await _context.Certificates
                .FirstOrDefaultAsync(c => c.Id == certificateId && c.EmployeeId == employeeId);

            if (certificate == null)
                return NotFound("Certificate not found for this employee!");

            var filePath = Path.Combine(_environment.WebRootPath, certificate.CertificateFilePath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            } 
            
            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();

            return Ok("Certificate deleted successfully."); 

        }
    }
}
