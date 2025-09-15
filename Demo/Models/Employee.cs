using System.ComponentModel.DataAnnotations;

namespace SkillNest.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public required string Location { get; set; }
        public required string Role { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public ICollection<Projects>? Projects { get; set; }
        public ICollection<Certificates>? Certificates { get; set; }

        public Resume? Resume { get; set; }
    }
}
