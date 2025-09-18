namespace SkillNest.DTO
{
    public class SkillResponseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; } // e.g., "Python", "C++", etc.
        public string? Proficiency { get; set; } // Optional: "Beginner", "Intermediate", "Expert"
    }
}
