namespace SkillNest.DTO
{
    public class EmployeeResponseDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Location { get; set; }
        public required string Role { get; set; }

        public List<SkillResponseDTO>? Skills { get; set; } = new List<SkillResponseDTO>();
        public List<ProjectResponseDTO>? Projects { get; set; } = new List<ProjectResponseDTO>();
        public List<CertificateResponseDTO> Certificates { get; set; } = new List<CertificateResponseDTO>();
    }
}
