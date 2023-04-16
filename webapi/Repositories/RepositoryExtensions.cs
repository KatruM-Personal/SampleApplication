namespace SampleAPI.Repositories
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            // register other repositories here
        }
    }
}
