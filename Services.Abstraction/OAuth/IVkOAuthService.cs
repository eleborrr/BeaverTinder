using Contracts;
using Contracts.Responses.Login;

namespace Services.Abstraction.OAuth;

public interface IVkOAuthService
{
    public Task<LoginResponseDto> AuthAsync(VkAuthDto authDto);

}