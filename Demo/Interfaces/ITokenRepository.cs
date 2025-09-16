namespace SkillNest.Interfaces
{
    public interface ITokenRepository
    {
        string CreateToken(int employeeId, string email, string role);
    }
}
