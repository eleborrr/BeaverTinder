using Contracts;
using Contracts.Responses.Payment;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation.Controllers;

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
    [Authorize]
    [HttpPost("pay")]
    public async Task<JsonResult> Pay([FromBody] PaymentRequestDto model)
    {
        if (!ModelState.IsValid)
            return Json(new PaymentResponseDto(PaymentResponseStatus.InvalidData));
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        model.UserId = user.Id;
        var payment = await _serviceManager.PaymentService.ProcessPayment(model);
        if (payment.StatusCode == PaymentResponseStatus.InvalidData)
            return Json(new PaymentResponseDto(PaymentResponseStatus.InvalidData));
        if(payment.StatusCode == PaymentResponseStatus.Fail)
            return Json(new PaymentResponseDto(PaymentResponseStatus.Fail));
        await _serviceManager.SubscriptionService.AddSubscriptionToUser(payment.SubsId, payment.UserId);
        return Json(new PaymentResponseDto(PaymentResponseStatus.Ok));
    }
}