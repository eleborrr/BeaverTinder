﻿using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public PaymentRepository(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;
    
    public async Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Payments.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Payment payment)
    {
        await _applicationDbContext.Payments.AddAsync(payment);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<Payment?> GetByPaymentIdAsync(int paymentId)
    {
        return await _applicationDbContext.Payments.FirstOrDefaultAsync(x => x.Id == paymentId);
    }
}