using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Bookings.Admin.ExpireBooking
{
    public sealed class ExpireBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClock _clock;

        public ExpireBookingHandler(
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            IClock clock)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _clock = clock;
        }

        public async Task HandleAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken)
                ?? throw new Exception("Booking not found.");

            booking.Expire(_clock.UtcNow);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
