using SmartParking.Domain.Entities;

namespace SmartParking.Application.Abstractions.Persistence
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment entity, CancellationToken cancellationToken = default);
        Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Payment>> GetByBookingIdAsync(Guid bookingId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Payment>> GetAllAsync(
    CancellationToken cancellationToken = default);
    }
}
