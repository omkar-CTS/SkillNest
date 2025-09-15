using System.ComponentModel.DataAnnotations;

namespace SkillNest.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
