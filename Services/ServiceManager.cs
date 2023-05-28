using System.Runtime.Intrinsics.X86;
using Contracts.Configs;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Persistence.Misc.Services.JwtGenerator;
using Services.Abstraction;
using Services.Abstraction.Account;
using Services.Abstraction.Chat;
using Services.Abstraction.Email;
using Services.Abstraction.FindBeaver;
using Services.Abstraction.Geolocation;
using Services.Abstraction.Likes;
using Services.Abstraction.OAuth;
using Services.Abstraction.PaymentService;
using Services.Abstraction.Subscriptions;
using Services.Abstraction.TwoFA;
using Services.Account;
using Services.Chat;
using Services.Email;
using Services.FindBeaver;
using Services.Geolocation;
using Services.Likes;
using Services.OAuth;
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
    private readonly Lazy<IAccountService> _accountService;
    private readonly Lazy<IVkOAuthService> _vkOAuthService;
    private readonly Lazy<IChatService> _chatService;

    public ServiceManager(UserManager<User> userManager, IOptions<EmailConfig> emailConfig, IRepositoryManager repositoryManager, IMemoryCache memoryCache,
        RoleManager<Role> roleManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator,
        IPasswordHasher<User> passwordHasher) 
    {
        _geolocationService = new Lazy<IGeolocationService>(() => new GeolocationService(repositoryManager, userManager));
        _emailService = new Lazy<IEmailService>(() => new EmailService(emailConfig));
        _twoFaService = new Lazy<ITwoFAService>(() => new TwoFAService(userManager, _emailService.Value));
        _likeService = new Lazy<ILikeService>(() => new LikeService(repositoryManager));
        _findBeaverService = new Lazy<IFindBeaverService>(() => new FindBeaverService(userManager, repositoryManager, memoryCache, roleManager , LikeService, GeolocationService));
        _paymentService = new Lazy<IPaymentService>(() => new PaymentService.PaymentService(repositoryManager));
        _subscriptionService = new Lazy<ISubscriptionService>(() => new SubscriptionService(repositoryManager, userManager));
        _accountService = new Lazy<IAccountService>(() => new AccountService(userManager, _emailService.Value, signInManager, jwtGenerator, GeolocationService, passwordHasher));
        _vkOAuthService = new Lazy<IVkOAuthService>(() => new VkOAuthService(repositoryManager, userManager, signInManager, jwtGenerator));
        _chatService = new Lazy<IChatService>(() => new ChatService(userManager, repositoryManager));
    }

    public IEmailService EmailService => _emailService.Value;
    public ITwoFAService TwoFaService => _twoFaService.Value;
    public ILikeService LikeService => _likeService.Value;
    public IFindBeaverService FindBeaverService => _findBeaverService.Value;
    public IPaymentService PaymentService => _paymentService.Value;
    public IGeolocationService GeolocationService => _geolocationService.Value;
    public ISubscriptionService SubscriptionService => _subscriptionService.Value;
    public IAccountService AccountService => _accountService.Value;
    public IVkOAuthService VkOAuthService => _vkOAuthService.Value;
    public IChatService ChatService => _chatService.Value;
}