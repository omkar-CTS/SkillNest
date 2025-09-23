using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillNest.Data;
using SkillNest.DTO;
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
            var certificates = await _context.Certificates
                .Where(c => c.EmployeeId == employeeId)
                .Select(c => new CertificateResponseDTO
                {
                   Id  = c.Id,
                   CertificateNumber = c.CertificateNumber,
                   Title = c.Name,
                   DateObtained = c.DateObtained,
                   FilePath = c.CertificateFilePath ?? string.Empty
                }).ToListAsync();
            if (!certificates.Any())
                return NotFound("No certificates found for the specified employee.");
            return Ok(certificates);
        }

        [HttpPost("{employeeId}")]
        public async Task<IActionResult> UploadCertificate(int employeeId, [FromForm] AddCertificationDTO addCertificationDTO)
        {
            if (addCertificationDTO.File == null || addCertificationDTO.File.Length == 0)
                return BadRequest("Please upload a certificate.");

            if (Path.GetExtension(addCertificationDTO.File.FileName).ToLower() != ".pdf")
                return BadRequest("Only PDF files are allowed.");

            var webRootPath  = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "Certificates");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{addCertificationDTO.File.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await addCertificationDTO.File.CopyToAsync(stream);
            }

            var certificate = new Certificates
            {
                EmployeeId = employeeId,
                Name = addCertificationDTO.Name,
                DateObtained = addCertificationDTO.Issuedate,
                ExpiryDate = addCertificationDTO.ExpiryDate,
                CertificateNumber = addCertificationDTO.CertificateNumber,
                CertificateFilePath = $"/Certificates/{uniqueFileName}"
            };

            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Certificate uploaded successfully!" });
        }

        [HttpPut("{certificationId}")]
        public async Task<IActionResult> UpdateCertificate(int certificationId, [FromForm] AddCertificationDTO updateCertificationDTO )
        {
            var certificate = await _context.Certificates.FindAsync(certificationId);
            if (certificate == null)
                return NotFound("Certificate not found!");

            certificate.Name = updateCertificationDTO.Name;
            certificate.DateObtained = updateCertificationDTO.Issuedate;
            certificate.ExpiryDate = updateCertificationDTO.ExpiryDate;
            certificate.CertificateNumber = updateCertificationDTO.CertificateNumber;

            if (updateCertificationDTO.File != null && updateCertificationDTO.File.Length > 0)
            {
                if (Path.GetExtension(updateCertificationDTO.File.FileName).ToLower() != ".pdf")
                    return BadRequest("Only PDF files are allowed.");

                var webRootPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var uploadsFolder = Path.Combine(webRootPath, "Certificates");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{updateCertificationDTO.File.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateCertificationDTO.File.CopyToAsync(stream);
                }

                certificate.CertificateFilePath = $"/Certificates/{uniqueFileName}";
            }
            _context.Certificates.Update(certificate);
            await _context.SaveChangesAsync();
            return Ok( new { Message = "Certification Updated Successfully!" } );
        }

        [HttpDelete("{certificateId}")]
        public async Task<IActionResult> DeleteCertificate(int certificateId)
        {
             var certificate = await _context.Certificates.FindAsync(certificateId);
             if (certificate == null)
                 return NotFound("Certificate not found!");

             _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Certification Deleted Successfully." });

        }
    }
}
