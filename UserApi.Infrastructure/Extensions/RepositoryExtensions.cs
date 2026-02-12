using Microsoft.Extensions.DependencyInjection;
using UserApi.Infrastructure.Interfaces;
using UserApi.Infrastructure.Repositories;

namespace UserApi.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();

            return services;
        }
    }
}
