using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Bookings.GetBooking
{
    public sealed class GetBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingResponse> HandleAsync(
            Guid bookingId,
            CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
            if (booking is null)
                throw new NotFoundException("Booking not found.");

            return new BookingResponse(
                booking.Id,
                booking.DriverId,
                booking.ParkingId,
                booking.StartTimeUtc,
                booking.EndTimeUtc,
                booking.Status.ToApiCode(),
                booking.ReservedPrice,
                booking.PriceCurrency,
                booking.ExpiresAtUtc,
                booking.CreatedAtUtc,
                booking.UpdatedAtUtc);
        }
    }
}
