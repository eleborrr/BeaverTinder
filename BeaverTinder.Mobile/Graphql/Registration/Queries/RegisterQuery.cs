using System.Web.Http.Results;
using BeaverTinder.Application.Dto.Authentication.Register;
using BeaverTinder.Application.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Mobile.Graphql.Registration.Queries;

public class RegisterQuery
{
    private readonly IServiceManager _manager;

    public RegisterQuery(IServiceManager manager)
    {
        _manager = manager;
    }


    public async Task<RegisterResponseDto> Register(RegisterRequestDto model)
    {
        return await _manager.AccountService.Register(model);
    }
}