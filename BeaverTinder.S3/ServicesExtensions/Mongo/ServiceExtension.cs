using BeaverTinder.Application.Configs;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.S3.Clients.MongoClient;

namespace BeaverTinder.S3.ServicesExtensions.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMongo(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MongoDbConfig>
            (configuration.GetSection("Mongo"));
        services.AddScoped<IMongoDbClient, MongoDbClient>();
        
        return services;
    }
    
}