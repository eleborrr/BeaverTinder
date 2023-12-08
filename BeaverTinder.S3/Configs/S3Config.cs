namespace BeaverTinder.S3.Configs;

public record S3Config(string BucketName, string SecretKey, string AccessKey, string Endpoint);