namespace SkillNest.DTO
{
    public class UpdateCertificationDTO
    {
        public required string Name { get; set; }
        public required DateOnly Issuedate { get; set; }
        public DateOnly? ExpiryDate { get; set; } // Nullable for certificates that don't expire
        public required string CertificateNumber { get; set; }
    }
}
