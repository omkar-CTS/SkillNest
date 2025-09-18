namespace SkillNest.DTO
{
    public class AddSkillDTO
    {
        public required string Skills { get; set; }
        public required string Proficiency { get; set; }
    }

    public class AddSkillListDTO
    {
        public int EmployeeId { get; set; }
        public required List<AddSkillDTO> Skills { get; set; }
    }
}

