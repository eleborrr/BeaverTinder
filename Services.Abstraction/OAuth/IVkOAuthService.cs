using Contracts;
using Contracts.Responses.Login;

namespace Services.Abstraction.OAuth;

public interface IVkOAuthService
{
    public Task<LoginResponseDto> OAuthCallback(VkUserDto vkUserDto);
    public Task<VkUserDto?> GetVkUserInfoAsync(VkAccessTokenDto accessToken);


}