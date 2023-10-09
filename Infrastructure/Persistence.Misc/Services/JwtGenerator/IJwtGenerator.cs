namespace Persistence.Misc.Services.JwtGenerator;

public interface IJwtGenerator
{
    public Task<string?> GenerateJwtToken(string userId);
}