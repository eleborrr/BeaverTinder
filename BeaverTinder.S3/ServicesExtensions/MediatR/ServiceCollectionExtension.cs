namespace BeaverTinder.S3.ServicesExtensions.MediatR;

public static class ServiceCollectionExtension
{ 
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);

        });
        return services;
    }
}