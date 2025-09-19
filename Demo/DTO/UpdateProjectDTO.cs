namespace SkillNest.DTO
{
    public class UpdateProjectDTO
    {
        public required String ProjectName { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required String Description { get; set; }
    }
}
