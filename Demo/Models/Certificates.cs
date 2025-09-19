namespace SkillNest.Models
{
    public class Certificates
    {
        public  int Id { get; set; }
        public required string Name { get; set; }
        public required DateOnly DateObtained { get; set; }
        public  DateOnly? ExpiryDate { get; set; } // Nullable for certificates that don't expire
        public required string CertificateNumber { get; set; }
        public string? CertificateFilePath { get; set; } 
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
       
    }
}
