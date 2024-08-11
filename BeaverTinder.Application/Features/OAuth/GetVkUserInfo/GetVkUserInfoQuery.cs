using BeaverTinder.Application.Dto.Vk;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace BeaverTinder.Application.Features.OAuth.GetVkUserInfo;

public record GetVkUserInfoQuery(VkAccessTokenDto AccessToken) : IQuery<VkUserDto?>;