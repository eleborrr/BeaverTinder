using BeaverTinder.Application.Dto.Authentication.Login;
using BeaverTinder.Application.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BeaverTinder.Mobile.Graphql.Login.Queries;

public class LoginQuery
{
    private readonly IServiceScopeFactory _scopeFactory;

    public LoginQuery(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto model)
    {
        using var scope = _scopeFactory.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IServiceManager>();

        return await manager.AccountService.Login(model);
    }
}