using SmartParking.Contracts.Bookings;

namespace SmartParking.Application.Abstractions.Services
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateAsync(BookingCreateRequest request, CancellationToken cancellationToken = default);
        Task<BookingResponse?> GetByIdAsync(Guid bookingId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DriverBookingListItemResponse>> GetDriverBookingsAsync(Guid driverId, CancellationToken cancellationToken = default);
        Task CancelAsync(Guid bookingId, string reason, CancellationToken cancellationToken = default);
    }
}
