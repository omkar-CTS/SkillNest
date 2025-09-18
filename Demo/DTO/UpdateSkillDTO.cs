namespace SkillNest.DTO
{
    public class UpdateSkillDTO
    {
        public int Id { get; set; }
        public required string Skills { get; set; }
        public string Proficiency { get; set; }
    }
}
