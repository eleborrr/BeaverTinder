using System.Text.Json;
using Application.OAuth.GetVkUserInfo;
using AspNet.Security.OAuth.Vkontakte;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Dto.Vk;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using Microsoft.AspNetCore.WebUtilities;

namespace BeaverTinder.Application.Features.OAuth.GetVkUserInfo;

public class GetVkUserInfoHandler : IQueryHandler<GetVkUserInfoQuery, VkUserDto?>
{
    private readonly HttpClient _client;

    public GetVkUserInfoHandler(HttpClient client)
    {
        _client = client;
    }

    public async Task<Result<VkUserDto?>> Handle(
        GetVkUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var accessToken = request.AccessToken;
        var query = new Dictionary<string, string?>()
        {
            ["fields"] = "screen_name, bdate, sex, status, about, photo_max_orig",
            ["access_token"] = accessToken.Token,
            ["v"] = "5.131",
        };
        var uri = QueryHelpers.AddQueryString(VkontakteAuthenticationDefaults.UserInformationEndpoint, query);
        var userInfo = await _client.GetAsync(uri,cancellationToken);
        var content = await userInfo.Content.ReadAsStringAsync(cancellationToken);
        var resp = JsonSerializer.Deserialize<VkResponseDto>(content);
        var user = resp!.Response!.FirstOrDefault();
        user!.Email = accessToken.Email;
        return new Result<VkUserDto?>(user, true);
    }
}