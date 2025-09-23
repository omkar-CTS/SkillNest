using Microsoft.AspNetCore.Http;
namespace SkillNest.DTO
{
    public class ResumeUploadDTO
    {
        public IFormFile File { get; set; } = null!;
        public int EmployeeId { get; set; }
    }
}
