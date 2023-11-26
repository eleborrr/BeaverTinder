using System.Security;
using Application.Subscription.AddSubscription;
using BeaverTinder.Application.Dto.Payment;
using BeaverTinder.Application.Features.Payment.AddPayment;
using BeaverTinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public PaymentController(UserManager<User> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }
    
    // GET
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("pay")]
    public async Task<JsonResult> Pay([FromBody] PaymentRequestDto model, CancellationToken cancellationToken)
    {
        var command = new AddPaymentCommand(model.UserId, model.CardNumber, model.Month, model.Amount, model.Year, model.Code, model.SubsId);
        
        var user = await GetUserFromJwt();
        command.UserId = user.Id;
        var response = await _mediator.Send(command, cancellationToken);
        if (response.IsFailure)
        {
            return new JsonResult(response);
        }
            //TODO: фича Subscriptions
        await _mediator.Send(
            new AddSubscriptionCommand(command.SubsId, command.UserId),
            cancellationToken);
        return Json(response);
    }
    
    //TODO: фича User??
    private async Task<User> GetUserFromJwt()
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == "Id");
        var user = await _userManager.FindByIdAsync(s!.Value);
        if (user is null)
            throw new SecurityException("user not found"); 
        return user;
    }
}   