namespace BeaverTinder.S3.Configs;

public record S3Config(string MainBucketName, string TemporaryBucketName, string SecretKey, string AccessKey, string Endpoint, string User, string Password);