using BeaverTinder.Application.Clients.MongoClient;
using BeaverTinder.Application.Configs;
using StackExchange.Redis;

namespace BeaverTinder.API.ServicesExtensions.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMongo(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MongoDbConfig>
            (configuration.GetSection("MongoDbConfig"));

        services.AddScoped<IMongoDbClient, MongoDbClient>();
        
        return services;
    }
    
}