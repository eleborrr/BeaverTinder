using BeaverTinder.Application.Services;
using BeaverTinder.Application.Services.Abstractions;

namespace BeaverTinder.Mobile.ServicesExtensions.CustomServices;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager , ServiceManager>();
        return services;
    }
}