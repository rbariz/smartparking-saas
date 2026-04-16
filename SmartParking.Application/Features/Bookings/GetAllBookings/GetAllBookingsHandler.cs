using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Bookings.GetAllBookings
{

    public sealed class GetAllBookingsHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public GetAllBookingsHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IReadOnlyList<BookingResponse>> HandleAsync(
            CancellationToken cancellationToken = default)
        {
            var items = await _bookingRepository.GetAllAsync(cancellationToken);

            return items
                .OrderByDescending(x => x.CreatedAtUtc)
                .Select(x => new BookingResponse(
                    x.Id,
                    x.DriverId,
                    x.ParkingId,
                    x.StartTimeUtc,
                    x.EndTimeUtc,
                    x.Status.ToApiCode(),
                    x.ReservedPrice,
                    x.PriceCurrency,
                    x.ExpiresAtUtc,
                    x.CreatedAtUtc,
                    x.UpdatedAtUtc))
                .ToList();
        }
    }
}
