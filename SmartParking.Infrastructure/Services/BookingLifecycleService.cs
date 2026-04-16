using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Services;
using SmartParking.Application.Abstractions.Time;

namespace SmartParking.Infrastructure.Services
{
    public sealed class BookingLifecycleService : IBookingLifecycleService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClock _clock;

        public BookingLifecycleService(
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            IClock clock)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _clock = clock;
        }

        public async Task<(int activated, int completed)> ProcessLifecycleAsync(
            CancellationToken cancellationToken = default)
        {
            var nowUtc = _clock.UtcNow;

            var activated = 0;
            var completed = 0;

            var toActivate = await _bookingRepository.GetBookingsToActivateAsync(nowUtc, cancellationToken);
            foreach (var booking in toActivate)
            {
                booking.MarkActive(nowUtc);
                activated++;
            }

            var toComplete = await _bookingRepository.GetBookingsToCompleteAsync(nowUtc, cancellationToken);
            foreach (var booking in toComplete)
            {
                booking.MarkCompleted(nowUtc);
                completed++;
            }

            if (activated > 0 || completed > 0)
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return (activated, completed);
        }
    }
}
