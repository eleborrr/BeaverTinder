using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;

    public SubscriptionController(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }

    [HttpGet("all")]
    public async Task<List<Subscription>> GetAll()
    {
       return await _serviceManager.SubscriptionService.GetAllAsync().ContinueWith(x => x.Result.ToList());
    }
}