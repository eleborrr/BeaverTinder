using BeaverTinder.Application.Dto.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Mobile.Graphql.Login.Queries;

public class LoginQuery
{
    private readonly IMediator _mediator;

    public LoginQuery(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task<JsonResult> Login(LoginRequestDto model)
    {
        // return Json(await _serviceManager.AccountService.Login(model, ModelState));
        throw new NotImplementedException();
    }
}