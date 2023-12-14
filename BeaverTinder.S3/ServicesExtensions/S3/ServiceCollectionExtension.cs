using BeaverTinder.S3.Configs;
using Minio;
using Minio.DataModel.Args;

namespace BeaverTinder.S3.ServicesExtensions.S3;

public static class ServiceCollectionExtension
{
    public static async Task<IServiceCollection> AddS3Client(this IServiceCollection services, IConfiguration configuration)
    {
        var minioConfiguration = configuration.GetSection("Minio");

        var s3Config = new S3Config(minioConfiguration["BucketName"],  minioConfiguration["TemporaryBucketName"],
            minioConfiguration["SecretKey"],
            minioConfiguration["AccessKey"], minioConfiguration["Endpoint"],
            minioConfiguration["User"], minioConfiguration["Password"]);

        services.AddSingleton(s3Config);
        
        var minioClient = new MinioClient().WithEndpoint(s3Config.Endpoint)
            .WithCredentials(s3Config.User, s3Config.Password)
            .Build();

        if (!await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(s3Config.MainBucketName)))
            await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(s3Config.MainBucketName));
        
        if (!await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(s3Config.TemporaryBucketName)))
            await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(s3Config.TemporaryBucketName));
        
        services.AddSingleton<IMinioClient>(o => minioClient);
        return services;
    }
}