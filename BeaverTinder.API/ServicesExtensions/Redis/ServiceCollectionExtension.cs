namespace BeaverTinder.API.ServicesExtensions.Redis;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRedis(this IServiceCollection services,
        IConfiguration configuration)
    {
        // var redisConfiguration = new RedisConfig
        // {
        //     Hostname = configuration["MessageBroker:Hostname"]!,
        //     Password = configuration["MessageBroker:Password"]!,
        //     Username = configuration["MessageBroker:Username"]!,
        //     Port = configuration["MessageBroker:Port"]!
        // };
        
        services.AddStackExchangeRedisCache(options => {
            options.Configuration = "localhost";
            options.InstanceName = "local";
        });
        return services;
    }
    
}