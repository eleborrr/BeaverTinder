using BeaverTinder.Application.Dto.Authentication.Login;
using BeaverTinder.Application.Dto.Authentication.Register;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Application.Services.Abstractions.Account;
using BeaverTinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BeaverTinder.Mobile.Graphql.Login.Queries;

public class Mutations
{
    private readonly IServiceScopeFactory _scopeFactory;
    public Mutations(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto model)
    {
        using var scope = _scopeFactory.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
        
        return await manager.AccountService.Login(model);
    }
    
    public async Task<RegisterResponseDto> Register(RegisterRequestDto model)
    {
        using var scope = _scopeFactory.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
        return await manager.AccountService.Register(model);
    }
}