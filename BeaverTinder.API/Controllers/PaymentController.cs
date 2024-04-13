using System.Security;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.TransactionManager;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Shared.Dto.Payment;
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
    private readonly ITransactionManager _transactionManager;

    public PaymentController(UserManager<User> userManager, IMediator mediator, ITransactionManager transactionManager)
    {
        _userManager = userManager;
        _mediator = mediator;
        _transactionManager = transactionManager;
    }
    
    // GET
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("pay")]
    public async Task<JsonResult> Pay([FromBody] PaymentRequestDto model, CancellationToken cancellationToken)
    {
        //Phase 1 - Prepare
        var isServicesReady = _transactionManager.CheckReadyServicesAsync();
        Console.WriteLine("beginning payment");
        var transactionState = new Result(false, "Services in pending state");
        if (isServicesReady)
        {
            var prepared = await _transactionManager.PrepareServicesAsync(model);
            if (prepared.IsSuccess)
            {
                transactionState = await _transactionManager.CommitAsync(model);
            }

            if (!transactionState.IsSuccess)
            {
                await _transactionManager.RollbackAsync(model);
                return Json(transactionState);
            }

            return Json(transactionState);
        }
        Console.WriteLine("services not ready");

        return Json(transactionState);
    }
    
    private async Task<User> GetUserFromJwt()
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == "Id");
        var user = await _userManager.FindByIdAsync(s!.Value);
        if (user is null)
            throw new SecurityException("user not found"); 
        return user;
    }
}   