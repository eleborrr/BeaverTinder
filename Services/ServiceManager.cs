using System.Runtime.Intrinsics.X86;
using Contracts.Configs;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Services.Abstraction.Email;
using Services.Abstraction.FindBeaver;
using Services.Abstraction.Geolocation;
using Services.Abstraction.Likes;
using Services.Abstraction.PaymentService;
using Services.Abstraction.Subscriptions;
using Services.Abstraction.TwoFA;
using Services.Email;
using Services.FindBeaver;
using Services.Geolocation;
using Services.Likes;
using Services.Subscriptions;
using Services.TwoFA;

namespace Services;

public class ServiceManager: IServiceManager
{
    private readonly Lazy<IEmailService> _emailService;
    private readonly Lazy<ITwoFAService> _twoFaService;
    private readonly Lazy<ILikeService> _likeService;
    private readonly Lazy<IGeolocationService> _geolocationService;
    private readonly Lazy<IFindBeaverService> _findBeaverService;
    private readonly Lazy<IPaymentService> _paymentService;
    private readonly Lazy<ISubscriptionService> _subscriptionService;

    public ServiceManager(UserManager<User> userManager, IOptions<EmailConfig> emailConfig, IRepositoryManager repositoryManager, 
        IMemoryCache memoryCache, RoleManager<Role> roleManager)  // ,
    {
        _geolocationService = new Lazy<IGeolocationService>(() => new GeolocationService(repositoryManager));
        _emailService = new Lazy<IEmailService>(() => new EmailService(emailConfig));
        _twoFaService = new Lazy<ITwoFAService>(() => new TwoFAService(userManager, _emailService.Value));
        _likeService = new Lazy<ILikeService>(() => new LikeService(repositoryManager));
        _findBeaverService = new Lazy<IFindBeaverService>(() => new FindBeaverService(userManager, repositoryManager, memoryCache, roleManager , LikeService));
        _paymentService = new Lazy<IPaymentService>(() => new PaymentService.PaymentService(repositoryManager));
        _subscriptionService = new Lazy<ISubscriptionService>(() => new SubscriptionService(repositoryManager, userManager));
    }

    public IEmailService EmailService => _emailService.Value;
    public ITwoFAService TwoFaService => _twoFaService.Value;
    public ILikeService LikeService => _likeService.Value;
    public IFindBeaverService FindBeaverService => _findBeaverService.Value;
    public IPaymentService PaymentService => _paymentService.Value;
    public IGeolocationService GeolocationService => _geolocationService.Value;
    public ISubscriptionService SubscriptionService => _subscriptionService.Value;
}