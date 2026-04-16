using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Services;
using SmartParking.Application.Abstractions.Time;
using SmartParking.Application.Common;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Bookings;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;
using SmartParking.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Bookings.CreateBooking
{

    public sealed class CreateBookingHandler
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IParkingRepository _parkingRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingPricingService _pricingService;
        private readonly IClock _clock;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookingHandler(
            IDriverRepository driverRepository,
            IParkingRepository parkingRepository,
            IBookingRepository bookingRepository,
            IBookingPricingService pricingService,
            IClock clock,
            IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _parkingRepository = parkingRepository;
            _bookingRepository = bookingRepository;
            _pricingService = pricingService;
            _clock = clock;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookingResponse> HandleAsync(
            BookingCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            var driver = await _driverRepository.GetByIdAsync(request.DriverId, cancellationToken);
            if (driver is null)
                throw new NotFoundException("Driver not found.");

            var parking = await _parkingRepository.GetByIdAsync(request.ParkingId, cancellationToken);
            if (parking is null)
                throw new NotFoundException("Parking not found.");

            if (parking.Status != ParkingStatus.Open)
                throw new ConflictException("Parking is closed.");

            if (parking.AvailableCount <= 0)
                throw new ConflictException("Parking has no available capacity.");

            var reservedPrice = _pricingService.CalculatePrice(
                parking.HourlyRate,
                request.StartTime,
                request.EndTime);

            var expiresAtUtc = _clock.UtcNow.AddMinutes(BookingRules.PendingPaymentExpirationMinutes);

            parking.ReserveCapacity();

            var booking = new Booking(
                request.DriverId,
                request.ParkingId,
                request.StartTime,
                request.EndTime,
                reservedPrice,
                parking.Currency,
                expiresAtUtc);

            await _bookingRepository.AddAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
