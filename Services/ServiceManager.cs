using Contracts.Configs;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Services.Abstraction.Email;
using Services.Abstraction.TwoFA;
using Services.Email;
using Services.TwoFA;

namespace Services;

public class ServiceManager: IServiceManager
{
    private readonly Lazy<IEmailService> _emailService;
    private readonly Lazy<ITwoFAService> _twoFaService;
    
    public ServiceManager(UserManager<User> userManager, IOptions<EmailConfig> emailConfig)  // IRepositoryManager repositoryManager,
    {

        _emailService = new Lazy<IEmailService>(() => new EmailService(emailConfig));
        _twoFaService = new Lazy<ITwoFAService>(() => new TwoFAService(userManager, _emailService.Value));
    }

    public IEmailService EmailService => _emailService.Value;
    public ITwoFAService TwoFaService => _twoFaService.Value;
}