namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        ILikeRepository LikeRepository { get; }
        
        IUnitOfWork UnitOfWork { get; }
    }
}
