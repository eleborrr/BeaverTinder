using System.Web.Http.Results;
using BeaverTinder.Application.Dto.Authentication.Register;
using BeaverTinder.Application.Services.Abstractions;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Mobile.Graphql.Registration.Queries;

public class RegisterMutation
{
    private readonly IServiceManager _manager;

    public RegisterMutation(IServiceManager manager)
    {
        _manager = manager;
    }


    public async Task<RegisterResponseDto> Register(RegisterRequestDto model)
    {
        return await _manager.AccountService.Register(model);
    }
}