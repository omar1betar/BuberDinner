using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Presistence;
using BuberDinner.Application.Common.Interface.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Authentication.Services;
using BuberDinner.Infrastructure.Presistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
