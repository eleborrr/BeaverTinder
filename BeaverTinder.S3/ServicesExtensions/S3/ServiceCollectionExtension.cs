using BeaverTinder.S3.Configs;
using Minio;
using Minio.DataModel.Args;

namespace BeaverTinder.S3.ServicesExtensions.S3;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddS3Client(this IServiceCollection services, IConfiguration configuration)
    {
        var minioConfiguration = configuration.GetSection("Minio");
        var user = minioConfiguration["User"];
        var password = minioConfiguration["Password"];
        var endpoint = minioConfiguration["Endpoint"];

        var s3Config = new S3Config(minioConfiguration["BucketName"], minioConfiguration["SecretKey"],
            minioConfiguration["AccessKey"], minioConfiguration["Endpoint"]);

        services.AddSingleton<S3Config>(s3Config);
        
        var minioClient = new MinioClient().WithEndpoint(endpoint)
            .WithCredentials(user, password)
            .Build();
        
        services.AddSingleton<IMinioClient>(o => minioClient);
        return services;
    }
}