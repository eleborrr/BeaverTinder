using System;
using Domain.Repositories;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<ILikeRepository> _lazyLikeRepository;

    public RepositoryManager(ApplicationDbContext dbContext)
    {
        _lazyLikeRepository = new Lazy<ILikeRepository>(() => new LikeRepository(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
    }

    public ILikeRepository LikeRepository => _lazyLikeRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}

