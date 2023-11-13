namespace BeaverTinder.Application.Helpers.JwtGenerator;

public interface IJwtGenerator
{
    public Task<string?> GenerateJwtToken(string userId);
}