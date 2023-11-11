using System.Security;
using Contracts.Dto.Payment;
using Domain.Entities;
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
    public PaymentController(UserManager<User> userManager, IServiceManager serviceManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }
    
    // GET
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("pay")]
    public async Task<JsonResult> Pay([FromBody] PaymentRequestDto model)
    {
        if (!ModelState.IsValid)
            return Json(new PaymentResponseDto(PaymentResponseStatus.InvalidData));
        var user = await GetUserFromJwt();
        model.UserId = user.Id;
        var payment = await _serviceManager.PaymentService.ProcessPayment(model);
        if (payment.StatusCode == PaymentResponseStatus.InvalidData)
            return Json(new PaymentResponseDto(PaymentResponseStatus.InvalidData));
        if(payment.StatusCode == PaymentResponseStatus.Fail)
            return Json(new PaymentResponseDto(PaymentResponseStatus.Fail));
        await _serviceManager.SubscriptionService.AddSubscriptionToUser(payment.SubsId, payment.UserId);
        return Json(new PaymentResponseDto(PaymentResponseStatus.Ok));
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