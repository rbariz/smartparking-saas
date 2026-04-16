using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Bookings.ListDriverBookings
{
    
    public sealed class ListDriverBookingsHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IParkingRepository _parkingRepository;
        private readonly IPaymentRepository _paymentRepository;


        public ListDriverBookingsHandler(
            IBookingRepository bookingRepository,
            IParkingRepository parkingRepository,
            IPaymentRepository paymentRepository)
        {
            _bookingRepository = bookingRepository;
            _parkingRepository = parkingRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<IReadOnlyList<DriverBookingListItemResponse>> HandleAsync(
        Guid driverId,
        CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetByDriverIdAsync(driverId, cancellationToken);

            var result = new List<DriverBookingListItemResponse>(bookings.Count);

            foreach (var booking in bookings)
            {
                var parking = await _parkingRepository.GetByIdAsync(booking.ParkingId, cancellationToken);
                var payments = await _paymentRepository.GetByBookingIdAsync(booking.Id, cancellationToken);
                var latestPayment = payments.OrderByDescending(x => x.CreatedAtUtc).FirstOrDefault();

                result.Add(new DriverBookingListItemResponse(
                    booking.Id,
                    booking.ParkingId,
                    parking?.Name ?? "Unknown parking",
                    booking.StartTimeUtc,
                    booking.EndTimeUtc,
                    booking.Status.ToApiCode(),
                    booking.ReservedPrice,
                    booking.PriceCurrency,
                    latestPayment?.Id,
                    latestPayment is null ? null : latestPayment.Status.ToApiCode()));
            }

            return result;
        }
    }
}
