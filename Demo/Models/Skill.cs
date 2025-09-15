using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillNest.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; } // e.g., "Python", "C++", etc.

        public string? Proficiency { get; set; } // Optional: "Beginner", "Intermediate", "Expert"
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
