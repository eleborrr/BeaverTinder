using System;
using Domain.Repositories;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<ILikeRepository> _lazyLikeRepository;
    private readonly Lazy<IGeolocationRepository> _lazyGeolocationRepository;

    public RepositoryManager(ApplicationDbContext dbContext)
    {
        _lazyGeolocationRepository = new Lazy<IGeolocationRepository>(() => new GeolocationRepository(dbContext));
        _lazyLikeRepository = new Lazy<ILikeRepository>(() => new LikeRepository(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
    }

    public ILikeRepository LikeRepository => _lazyLikeRepository.Value;
    public IGeolocationRepository GeolocationRepository => _lazyGeolocationRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}

