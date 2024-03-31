namespace BeaverTinder.Domain.Repositories.Abstractions
{
    public interface IRepositoryManager
    {
        ILikeRepository LikeRepository { get; }
        IGeolocationRepository GeolocationRepository { get; }
        IUnitOfWork UnitOfWork { get; }
        IUserToVkRepository UserToVkRepository { get; }
        IRoomRepository RoomRepository { get; }
        IMessageRepository MessageRepository { get; }
        ISupportChatMessageRepository SupportChatMessageRepository { get; }
        ISupportRoomRepository SupportRoomRepository { get; }
    }
}
