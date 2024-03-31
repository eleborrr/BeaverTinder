using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Infrastructure.Database.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<ILikeRepository> _lazyLikeRepository;
    private readonly Lazy<IGeolocationRepository> _lazyGeolocationRepository;
    private readonly Lazy<IUserToVkRepository> _lazyUserToVkRepository;
    private readonly Lazy<IRoomRepository> _lazyRoomRepository;
    private readonly Lazy<IMessageRepository> _lazyMessageRepository;
    private readonly Lazy<ISupportChatMessageRepository> _lazySupportChatMessageRepository;
    private readonly Lazy<ISupportRoomRepository> _lazySupportRoomRepository;
    private readonly Lazy<IFileToMessageRepository> _lazyFileToMessageRepository;
    public RepositoryManager(ApplicationDbContext dbContext)
    {
        _lazyFileToMessageRepository = new Lazy<IFileToMessageRepository>(() => new FileToMessageRepository(dbContext));
        _lazyGeolocationRepository = new Lazy<IGeolocationRepository>(() => new GeolocationRepository(dbContext));
        _lazyLikeRepository = new Lazy<ILikeRepository>(() => new LikeRepository(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        _lazyUserToVkRepository = new Lazy<IUserToVkRepository>(() => new UserToVkRepository(dbContext));
        _lazyMessageRepository = new Lazy<IMessageRepository>(() => new MessageRepository(dbContext));
        _lazyRoomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(dbContext));
        _lazySupportChatMessageRepository =
            new Lazy<ISupportChatMessageRepository>(() => new SupportChatMessageRepository(dbContext));
        _lazySupportRoomRepository = new Lazy<ISupportRoomRepository>(() => new SupportRoomRepository(dbContext));
    }

    public IFileToMessageRepository FileToMessageRepository => _lazyFileToMessageRepository.Value;
    public ILikeRepository LikeRepository => _lazyLikeRepository.Value;
    public IGeolocationRepository GeolocationRepository => _lazyGeolocationRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    public IUserToVkRepository UserToVkRepository => _lazyUserToVkRepository.Value;
    public IRoomRepository RoomRepository => _lazyRoomRepository.Value;
    public IMessageRepository MessageRepository => _lazyMessageRepository.Value;
    public ISupportChatMessageRepository SupportChatMessageRepository => _lazySupportChatMessageRepository.Value;
    public ISupportRoomRepository SupportRoomRepository => _lazySupportRoomRepository.Value;
}

