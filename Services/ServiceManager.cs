using Contracts.Configs;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Services.Abstraction.Account;
using Services.Abstraction.Email;
using Services.Abstraction.FindBeaver;
using Services.Abstraction.Geolocation;
using Services.Abstraction.Likes;
using Services.Abstraction.TwoFA;
using Services.Account;
using Services.Email;
using Services.FindBeaver;
using Services.Geolocation;
using Services.Likes;
using Services.TwoFA;

namespace Services;

public class ServiceManager: IServiceManager
{
    private readonly Lazy<IEmailService> _emailService;
    private readonly Lazy<ITwoFAService> _twoFaService;
    private readonly Lazy<ILikeService> _likeService;
    private readonly Lazy<IGeolocationService> _geolocationService;
    private readonly Lazy<IFindBeaverService> _findBeaverService;
    private readonly Lazy<IAccountService> _accountService;

    public ServiceManager(UserManager<User> userManager, IOptions<EmailConfig> emailConfig, IRepositoryManager repositoryManager, IMemoryCache memoryCache, RoleManager<Role> roleManager, SignInManager<User> signInManager)  // ,
    {
        _geolocationService = new Lazy<IGeolocationService>(() => new GeolocationService(repositoryManager));
        _emailService = new Lazy<IEmailService>(() => new EmailService(emailConfig));
        _twoFaService = new Lazy<ITwoFAService>(() => new TwoFAService(userManager, _emailService.Value));
        _likeService = new Lazy<ILikeService>(() => new LikeService(repositoryManager));
        _findBeaverService = new Lazy<IFindBeaverService>(() => new FindBeaverService(userManager, repositoryManager, memoryCache, roleManager , LikeService));
        _accountService =
            new Lazy<IAccountService>(() => new AccountService(userManager, _emailService.Value, signInManager));
    }

    public IEmailService EmailService => _emailService.Value;
    public ITwoFAService TwoFaService => _twoFaService.Value;
    public ILikeService LikeService => _likeService.Value;
    public IFindBeaverService FindBeaverService => _findBeaverService.Value;
    public IGeolocationService GeolocationService => _geolocationService.Value;
    public IAccountService AccountService => _accountService.Value;
}