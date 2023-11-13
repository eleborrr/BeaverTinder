using AspNet.Security.OAuth.Vkontakte;
using Contracts.Dto.MediatR;
using Contracts.Dto.Vk;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using Services.Abstraction.Cqrs.Queries;

namespace Application.OAth.GetVkUserInfo;

public class GetVkUserInfoHandler : IQueryHandler<GetVkUserInfoQuery, VkUserDto?>
{
    private readonly HttpClient _client;

    public GetVkUserInfoHandler(HttpClient client)
    {
        _client = client;
    }

    public async Task<Result<VkUserDto?>> Handle(GetVkUserInfoQuery request, CancellationToken cancellationToken)
    {
        var accessToken = request.AccessToken;
        var query = new Dictionary<string, string?>()
        {
            ["fields"] = "screen_name, bdate, sex, status, about, photo_max_orig",
            ["access_token"] = accessToken.Token,
            ["v"] = "5.131",
        };
        var uri = QueryHelpers.AddQueryString(VkontakteAuthenticationDefaults.UserInformationEndpoint, query);
        var userInfo = await _client.GetAsync(uri);
        var content = await userInfo.Content.ReadAsStringAsync();
        var resp = JsonSerializer.Deserialize<VkResponseDto>(content);
        var user = resp!.Response!.FirstOrDefault();
        user!.Email = accessToken.Email;
        return new Result<VkUserDto?>(user, true);
    }
}