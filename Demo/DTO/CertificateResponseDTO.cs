namespace SkillNest.DTO
{
    public class CertificateResponseDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string CertificateNumber { get; set; }
        public DateOnly DateObtained { get; set; }
    }
}
