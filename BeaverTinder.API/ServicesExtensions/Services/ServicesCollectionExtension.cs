using BeaverTinder.Application.Configs;
using BeaverTinder.Application.Helpers.JwtGenerator;
using BeaverTinder.Application.Services;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Infrastructure.Database.Repositories;
using MassTransit;

namespace BeaverTinder.API.ServicesExtensions.Services;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IServiceManager , ServiceManager>();
        services.AddScoped<HttpClient>();

        services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
        services.Configure<EmailConfig>(configuration.GetSection("SmtpSettings"));

        return services;
    }
}