using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;

namespace SmartParking.Infrastructure.Persistence.Repositories
{
    public sealed class BookingRepository : IBookingRepository
    {
        private readonly SmartParkingDbContext _dbContext;

        public BookingRepository(SmartParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Booking entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Bookings.AddAsync(entity, cancellationToken);
        }

        public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Booking>> GetByDriverIdAsync(Guid driverId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Bookings
                .Where(x => x.DriverId == driverId)
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<Booking>> GetExpiredPendingPaymentBookingsAsync(
        DateTime nowUtc,
        CancellationToken cancellationToken = default)
        {
            return await _dbContext.Bookings
                .Where(x => x.Status == BookingStatus.PendingPayment && x.ExpiresAtUtc <= nowUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Booking>> GetBookingsToActivateAsync(
    DateTime nowUtc,
    CancellationToken cancellationToken = default)
        {
            return await _dbContext.Bookings
                .Where(x => x.Status == BookingStatus.Confirmed
                            && x.StartTimeUtc <= nowUtc
                            && x.EndTimeUtc > nowUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Booking>> GetBookingsToCompleteAsync(
            DateTime nowUtc,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Bookings
                .Where(x =>
                    (x.Status == BookingStatus.Active || x.Status == BookingStatus.Confirmed)
                    && x.EndTimeUtc <= nowUtc)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Booking>> GetAllAsync(
    CancellationToken cancellationToken = default)
        {
            return await _dbContext.Bookings
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync(cancellationToken);
        }
    }
}
