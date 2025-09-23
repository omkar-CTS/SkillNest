using Microsoft.AspNetCore.Http;
namespace SkillNest.DTO
{
    public class AddCertificationDTO
    {
        public required string Name { get; set; }
        public required IFormFile File { get; set; } 
        public required DateOnly Issuedate { get; set; }
        public DateOnly? ExpiryDate { get; set; } // Nullable for certificates that don't expire
        public required string CertificateNumber { get; set; }
        public required int EmployeeId { get; set; }
    }
}
