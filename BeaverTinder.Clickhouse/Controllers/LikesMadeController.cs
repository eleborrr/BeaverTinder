using BeaverTinder.Clickhouse.Contracts;
using BeaverTinder.Clickhouse.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Clickhouse.Controllers;

[ApiController]
[Route("[controller]")]
public class LikesMadeController : Controller
{
    private readonly IClickhouseService _clickhouseService;

    public LikesMadeController(IClickhouseService clickhouseService)
    {
        _clickhouseService = clickhouseService;
    }

    [HttpGet]
    [Route("/getMadeLikes")]
    public async Task<JsonResult> GetMadeLikes()
    {
        var result = new AllLikesMadeDto();
        var todayLikes = await _clickhouseService.GetCurrentDayLikes(DateTime.Now);
        var allDaysLikes = await _clickhouseService.GetAllDaysLikes();
        result.TodayLikes = todayLikes;
        result.AllDaysLikes = allDaysLikes;
        return Json(result);
    }
}