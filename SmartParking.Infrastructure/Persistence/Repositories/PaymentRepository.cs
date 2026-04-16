using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence.Repositories
{
    public sealed class PaymentRepository : IPaymentRepository
    {
        private readonly SmartParkingDbContext _dbContext;

        public PaymentRepository(SmartParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Payment entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Payments.AddAsync(entity, cancellationToken);
        }

        public Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Payments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Payment>> GetByBookingIdAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Payments
                .Where(x => x.BookingId == bookingId)
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Payment>> GetAllAsync(
    CancellationToken cancellationToken = default)
        {
            return await _dbContext.Payments
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync(cancellationToken);
        }
    }
}
