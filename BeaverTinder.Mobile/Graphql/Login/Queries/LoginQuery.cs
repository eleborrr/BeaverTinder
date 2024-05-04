using BeaverTinder.Application.Dto.Authentication.Login;
using BeaverTinder.Application.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BeaverTinder.Mobile.Graphql.Login.Queries;

public class LoginQuery
{
    private readonly IServiceManager _manager;

    public LoginQuery(IServiceManager manager)
    {
        _manager = manager;
    }


    public async Task<LoginResponseDto> Login(LoginRequestDto model)
    {
        return await _manager.AccountService.Login(model);
    }
}