using Contracts.Dto.Authentication.Login;
using Contracts.Dto.Vk;

namespace Services.Abstraction.OAuth;

public interface IVkOAuthService
{
    public Task<LoginResponseDto> OAuthCallback(VkUserDto vkUserDto);
    public Task<VkUserDto?> GetVkUserInfoAsync(VkAccessTokenDto accessToken);


}