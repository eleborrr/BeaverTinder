using System;
using Domain.Repositories;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<ILikeRepository> _lazyLikeRepository;
    private readonly Lazy<IGeolocationRepository> _lazyGeolocationRepository;
    private readonly Lazy<IPaymentRepository> _lazyPaymentRepository;
    private readonly Lazy<ISubscriptionRepository> _lazySubscriptionRepository;
    private readonly Lazy<IUserSubscriptionRepository> _lazyUserSubscriptionRepository;
    public RepositoryManager(ApplicationDbContext dbContext)
    {
        _lazyGeolocationRepository = new Lazy<IGeolocationRepository>(() => new GeolocationRepository(dbContext));
        _lazyLikeRepository = new Lazy<ILikeRepository>(() => new LikeRepository(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        _lazyPaymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(dbContext));
        _lazySubscriptionRepository = new Lazy<ISubscriptionRepository>(() => new SubscriptionRepository(dbContext));
        _lazyUserSubscriptionRepository =
            new Lazy<IUserSubscriptionRepository>(() => new UserSubscriptionRepository(dbContext));
    }

    public ILikeRepository LikeRepository => _lazyLikeRepository.Value;
    public IGeolocationRepository GeolocationRepository => _lazyGeolocationRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    public IPaymentRepository PaymentRepository => _lazyPaymentRepository.Value;
    public ISubscriptionRepository SubscriptionRepository => _lazySubscriptionRepository.Value;
    public IUserSubscriptionRepository UserSubscriptionRepository => _lazyUserSubscriptionRepository.Value;
}

