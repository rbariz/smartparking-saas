using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Services;
using SmartParking.Application.Common;
using SmartParking.Contracts.Parkings;
using SmartParking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Parkings.GetParkingQuote
{

    public sealed class GetParkingQuoteHandler
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IBookingPricingService _pricingService;

        public GetParkingQuoteHandler(
            IParkingRepository parkingRepository,
            IBookingPricingService pricingService)
        {
            _parkingRepository = parkingRepository;
            _pricingService = pricingService;
        }

        public async Task<ParkingQuoteResponse> HandleAsync(
            Guid parkingId,
            DateTime startTime,
            DateTime endTime,
            CancellationToken cancellationToken = default)
        {
            var parking = await _parkingRepository.GetByIdAsync(parkingId, cancellationToken);
            if (parking is null)
                throw new NotFoundException("Parking not found.");

            if (parking.Status != ParkingStatus.Open)
                throw new ConflictException("Parking is closed.");

            var billableHours = _pricingService.CalculateBillableHours(startTime, endTime);
            var estimatedPrice = _pricingService.CalculatePrice(parking.HourlyRate, startTime, endTime);

            return new ParkingQuoteResponse(
                parking.Id,
                startTime,
                endTime,
                billableHours,
                parking.HourlyRate,
                estimatedPrice,
                parking.Currency);
        }
    }
}
