﻿namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        ILikeRepository LikeRepository { get; }
        
        IGeolocationRepository GeolocationRepository { get; }
        
        IUnitOfWork UnitOfWork { get; }
        
        IPaymentRepository PaymentRepository { get; }
        ISubscriptionRepository SubscriptionRepository { get; }
        IUserSubscriptionRepository UserSubscriptionRepository { get; }
    }
}
