using BeaverTinder.Application.Configs;
using BeaverTinder.Application.Helpers.JwtGenerator;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Application.Services.Abstractions.Account;
using BeaverTinder.Application.Services.Abstractions.Chat;
using BeaverTinder.Application.Services.Abstractions.Email;
using BeaverTinder.Application.Services.Abstractions.FindBeaver;
using BeaverTinder.Application.Services.Abstractions.Geolocation;
using BeaverTinder.Application.Services.Abstractions.Likes;
using BeaverTinder.Application.Services.Abstractions.OAuth;
using BeaverTinder.Application.Services.Abstractions.Payments;
using BeaverTinder.Application.Services.Abstractions.Subscriptions;
using BeaverTinder.Application.Services.Abstractions.SupportChat;
using BeaverTinder.Application.Services.Abstractions.TwoFA;
using BeaverTinder.Application.Services.Account;
using BeaverTinder.Application.Services.Chat;
using BeaverTinder.Application.Services.Email;
using BeaverTinder.Application.Services.FindBeaver;
using BeaverTinder.Application.Services.Geolocation;
using BeaverTinder.Application.Services.Likes;
using BeaverTinder.Application.Services.OAuth;
using BeaverTinder.Application.Services.Subscriptions;
using BeaverTinder.Application.Services.SupportChat;
using BeaverTinder.Application.Services.TwoFA;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BeaverTinder.Application.Services;

public class ServiceManager: IServiceManager
{
    private readonly Lazy<IEmailService> _emailService;
    private readonly Lazy<ITwoFaService> _twoFaService;
    private readonly Lazy<ILikeService> _likeService;
    private readonly Lazy<IGeolocationService> _geolocationService;
    private readonly Lazy<IFindBeaverService> _findBeaverService;
    private readonly Lazy<IPaymentService> _paymentService;
    private readonly Lazy<ISubscriptionService> _subscriptionService;
    private readonly Lazy<IAccountService> _accountService;
    private readonly Lazy<IVkOAuthService> _vkOAuthService;
    private readonly Lazy<IChatService> _chatService;
    private readonly Lazy<ISupportChatService> _supportChatService;

    public ServiceManager(UserManager<User> userManager,
        IOptions<EmailConfig> emailConfig,
        IRepositoryManager repositoryManager,
        IMemoryCache memoryCache,
        SignInManager<User> signInManager, 
        IJwtGenerator jwtGenerator,
        IPasswordHasher<User> passwordHasher, 
        HttpClient client,
        IBus publishEndpoint)
    {
        _geolocationService = new Lazy<IGeolocationService>(() => new GeolocationService(repositoryManager));
        _emailService = new Lazy<IEmailService>(() => new EmailService(emailConfig));
        _twoFaService = new Lazy<ITwoFaService>(() => new TwoFaService(userManager, _emailService.Value));
        _likeService = new Lazy<ILikeService>(() => new LikeService(repositoryManager));
        _findBeaverService = new Lazy<IFindBeaverService>(() => new FindBeaverService(userManager, repositoryManager, memoryCache, LikeService, GeolocationService));
        _paymentService = new Lazy<IPaymentService>(() => new global::BeaverTinder.Application.Services.PaymentService.PaymentService(repositoryManager));
        _subscriptionService = new Lazy<ISubscriptionService>(() => new SubscriptionService(repositoryManager, userManager));
        _vkOAuthService = new Lazy<IVkOAuthService>(() => new VkOAuthService(repositoryManager, userManager, signInManager, jwtGenerator, client, GeolocationService));
        _accountService = new Lazy<IAccountService>(() => new AccountService(userManager, _emailService.Value, signInManager, jwtGenerator, GeolocationService, passwordHasher));
        _chatService = new Lazy<IChatService>(() => new ChatService(repositoryManager));
        _supportChatService = new Lazy<ISupportChatService>(() =>
            new SupportChatService(repositoryManager, publishEndpoint, userManager));
    }

    public IEmailService EmailService => _emailService.Value;
    public ITwoFaService TwoFaService => _twoFaService.Value;
    public ILikeService LikeService => _likeService.Value;
    public IFindBeaverService FindBeaverService => _findBeaverService.Value;
    public IPaymentService PaymentService => _paymentService.Value;
    public IGeolocationService GeolocationService => _geolocationService.Value;
    public ISubscriptionService SubscriptionService => _subscriptionService.Value;
    public IAccountService AccountService => _accountService.Value;
    public IVkOAuthService VkOAuthService => _vkOAuthService.Value;
    public IChatService ChatService => _chatService.Value;
    public ISupportChatService SupportChatService => _supportChatService.Value;
}