using System.ComponentModel.DataAnnotations;

namespace SkillNest.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }

        [MaxLength(1000)]
        public required string? Description { get; set; }

        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
