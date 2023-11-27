using Minio;

namespace BeaverTinder.S3.ServicesExtensions.S3;

public static class ServiceCollectionExtension
{
    private static readonly IConfiguration _configuration;
    
    public static IServiceCollection AddS3Client(this IServiceCollection services, IConfiguration configuration)
    {
        var minioConfiguration = configuration.GetSection("Minio");
        var accessKey = minioConfiguration["AccessKey"];
        var secretKey = minioConfiguration["SecretKey"];
        var endpoint = minioConfiguration["Endpoint"];

        var minioClient = new MinioClient().WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();

        services.AddSingleton<IMinioClient>(o => minioClient);
        return services;
    }
}