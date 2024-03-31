// using BeaverTinder.Domain.Entities;
// using BeaverTinder.Domain.Repositories.Abstractions;
// using Microsoft.EntityFrameworkCore;
//
// namespace BeaverTinder.Infrastructure.Database.Repositories;
//
// public class PaymentRepository : IPaymentRepository
// {
//     private readonly ApplicationDbContext _applicationDbContext;
//     
//     public PaymentRepository(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;
//     
//     public async Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken)
//     {
//         return await _applicationDbContext.Payments.ToListAsync(cancellationToken);
//     }
//
//     public async Task<int> AddAsync(Payment payment)
//     {
//         await _applicationDbContext.Payments.AddAsync(payment);
//         await _applicationDbContext.SaveChangesAsync();
//         return payment.Id;
//     }
//
//     public async Task<Payment?> GetByPaymentIdAsync(int paymentId)
//     {
//         return await _applicationDbContext.Payments.FirstOrDefaultAsync(x => x.Id == paymentId);
//     }
// }