using System.Web.Http.Results;
using BeaverTinder.Application.Dto.Authentication.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Mobile.Graphql.Registration.Queries;

public class RegisterQuery
{
    private readonly IMediator _mediator;

    public RegisterQuery(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task<JsonResult> Register(RegisterRequestDto model)
    {
        // return Json(await _serviceManager.AccountService.Register(model, ModelState));
        throw new NotImplementedException();
    }
}