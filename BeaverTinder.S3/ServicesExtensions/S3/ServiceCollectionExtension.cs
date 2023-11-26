using Minio;

namespace BeaverTinder.S3.ServicesExtensions.S3;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddS3Client(this IServiceCollection services)
    {
        var accessKey = "F7l1mZ14Pno43XicMUHY";
        var secretKey = "Aaz371CWmcr650RLk6xRJSeG0rPw9CB2okThDlwX";
        var endpoint = "localhost:9000";
        
        var minioClient = new MinioClient().WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();

        services.AddSingleton<IMinioClient>(o => minioClient);
        return services;
    }
}