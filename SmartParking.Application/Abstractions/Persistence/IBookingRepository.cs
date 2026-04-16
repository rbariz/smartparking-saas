using SmartParking.Domain.Entities;

namespace SmartParking.Application.Abstractions.Persistence
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking entity, CancellationToken cancellationToken = default);
        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Booking>> GetByDriverIdAsync(Guid driverId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Booking>> GetExpiredPendingPaymentBookingsAsync(
        DateTime nowUtc,
        CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Booking>> GetBookingsToActivateAsync(
            DateTime nowUtc,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Booking>> GetBookingsToCompleteAsync(
            DateTime nowUtc,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Booking>> GetAllAsync(
            CancellationToken cancellationToken = default);
    }
}
