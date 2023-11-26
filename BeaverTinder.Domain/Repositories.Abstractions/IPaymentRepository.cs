using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface IPaymentRepository
{
    public Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken);
    public Task<int> AddAsync(Payment payment);
    public Task<Payment?> GetByPaymentIdAsync(int paymentId);
}