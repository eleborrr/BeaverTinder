using System.Security;
using Application.Payment.AddPayment;
using Contracts.Dto.Payment;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    private readonly IMediator _mediator;
    public PaymentController(UserManager<User> userManager, IServiceManager serviceManager, IMediator mediator)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
        _mediator = mediator;
    }
    
    // GET
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("pay")]
    public async Task<JsonResult> Pay([FromBody] PaymentRequestDto model, CancellationToken cancellationToken)
    {
        var command = new AddPaymentCommand(model.UserId, model.CardNumber, model.Month, model.Amount, model.Year, model.Code, model.SubsId);
        
        var user = await GetUserFromJwt();
        model.UserId = user.Id;
        var response = await _mediator.Send(command, cancellationToken);
        //TODO: фича Subscriptions
        await _serviceManager.SubscriptionService.AddSubscriptionToUser(payment.SubsId, payment.UserId);
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