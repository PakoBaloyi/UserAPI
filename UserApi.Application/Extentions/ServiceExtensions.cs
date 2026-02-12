using Microsoft.Extensions.DependencyInjection;
using UserApi.Application.Interfaces;
using UserApi.Application.Services;

namespace UserApi.Application.Extentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<TokenService>();

            return services;
        }
    }
}