using BeaverTinder.Application.Dto.Authentication.Login;
using BeaverTinder.Application.Dto.Vk;

namespace BeaverTinder.Application.Services.Abstractions.OAuth;

public interface IVkOAuthService
{
    public Task<LoginResponseDto> OAuthCallback(VkUserDto vkUserDto);
    public Task<VkUserDto?> GetVkUserInfoAsync(VkAccessTokenDto accessToken);


}