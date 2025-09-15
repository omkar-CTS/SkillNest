namespace SkillNest.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public required string FilePath { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
