using Contracts.Dto.Vk;
using Services.Abstraction.Cqrs.Queries;

namespace Application.OAuth.GetVkUserInfo;

public record GetVkUserInfoQuery(VkAccessTokenDto AccessToken) : IQuery<VkUserDto?>;