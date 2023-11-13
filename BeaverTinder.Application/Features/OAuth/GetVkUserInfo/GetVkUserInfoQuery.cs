using BeaverTinder.Application.Dto.Vk;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace Application.OAuth.GetVkUserInfo;

public record GetVkUserInfoQuery(VkAccessTokenDto AccessToken) : IQuery<VkUserDto?>;