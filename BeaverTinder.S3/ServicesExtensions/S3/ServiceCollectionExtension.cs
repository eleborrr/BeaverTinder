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

        var minioClient = new MinioClient().WithEndpoint(endpoint)
            .WithCredentials(user, password)
            .Build();
        //
        // var bucketName = "my-bucket";
        //
        // var putObjectArgs = new MakeBucketArgs()
        //     .WithBucket(bucketName);
        //
        // minioClient.MakeBucketAsync(putObjectArgs).Wait();
        
        
        services.AddSingleton<IMinioClient>(o => minioClient);
        return services;
    }
}