using Contracts.Dto.Vk;
using Services.Abstraction.Cqrs.Queries;

namespace Application.OAth.GetVkUserInfo;

public record GetVkUserInfoQuery(VkAccessTokenDto AccessToken) : IQuery<VkUserDto?>;