namespace SkillNest.DTO
{
    public class ProjectAddDTO
    {
        public required String ProjectName { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required String Description { get; set; }
    }
}
