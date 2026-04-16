using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Services;
using SmartParking.Application.Abstractions.Time;
using SmartParking.Domain.Enums;

namespace SmartParking.Infrastructure.Services
{
    public sealed class BookingExpirationService : IBookingExpirationService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IParkingRepository _parkingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClock _clock;

        public BookingExpirationService(
            IBookingRepository bookingRepository,
            IParkingRepository parkingRepository,
            IUnitOfWork unitOfWork,
            IClock clock)
        {
            _bookingRepository = bookingRepository;
            _parkingRepository = parkingRepository;
            _unitOfWork = unitOfWork;
            _clock = clock;
        }

        public async Task<int> ExpirePendingPaymentBookingsAsync(CancellationToken cancellationToken = default)
        {
            var nowUtc = _clock.UtcNow;

            var expiredBookings = await _bookingRepository
                .GetExpiredPendingPaymentBookingsAsync(nowUtc, cancellationToken);

            if (expiredBookings.Count == 0)
                return 0;

            var count = 0;

            foreach (var booking in expiredBookings)
            {
                if (booking.Status != BookingStatus.PendingPayment)
                    continue;

                var parking = await _parkingRepository.GetByIdAsync(booking.ParkingId, cancellationToken);
                if (parking is null)
                    continue;

                booking.Expire(nowUtc);
                parking.ReleaseCapacity();

                count++;
            }

            if (count > 0)
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            return count;
        }
    }
}
