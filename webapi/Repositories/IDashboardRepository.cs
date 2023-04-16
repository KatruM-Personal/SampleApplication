namespace SampleAPI.Repositories
{
    public interface IDashboardRepository
    {
        Task<string> GetEmail(string email);
    }
}
