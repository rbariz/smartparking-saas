using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Services;
using SmartParking.Application.Common.Geo;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Parkings;
using SmartParking.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Parkings.SearchParkings
{
    

    public sealed class SearchParkingsHandler
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IBookingPricingService _pricingService;

        public SearchParkingsHandler(
            IParkingRepository parkingRepository,
            IBookingPricingService pricingService)
        {
            _parkingRepository = parkingRepository;
            _pricingService = pricingService;
        }

        public async Task<IReadOnlyList<ParkingSearchItemResponse>> HandleAsync(
    ParkingSearchRequest request,
    CancellationToken cancellationToken = default)
        {
            var parkings = await _parkingRepository.SearchNearbyAsync(
                request.Latitude,
                request.Longitude,
                request.RadiusMeters,
                request.OpenOnly,
                cancellationToken);

            var items = new List<ParkingSearchItemResponse>(parkings.Count);

            foreach (var parking in parkings)
            {
                var distanceMeters = DistanceCalculator.CalculateDistanceMeters(
                    request.Latitude,
                    request.Longitude,
                    parking.Latitude,
                    parking.Longitude);

                if (request.RadiusMeters > 0 && distanceMeters > request.RadiusMeters)
                    continue;

                decimal estimatedPrice = 0m;

                if (request.StartTime.HasValue && request.EndTime.HasValue)
                {
                    estimatedPrice = _pricingService.CalculatePrice(
                        parking.HourlyRate,
                        request.StartTime.Value,
                        request.EndTime.Value);
                }
                var status = parking.AvailableCount == 0
                        ? "full"
                        : parking.Status.ToApiCode();

                items.Add(new ParkingSearchItemResponse(
                    parking.Id,
                    parking.Name,
                    parking.Address,
                    parking.Latitude,
                    parking.Longitude,
                    distanceMeters,
                    status,
                    parking.AvailableCount,
                    parking.TotalCapacity,
                    parking.HourlyRate,
                    parking.Currency,
                    estimatedPrice));
            }

            items = request.SortBy?.ToLowerInvariant() switch
            {
                "price" => items
                    .OrderBy(x => x.EstimatedPrice)
                    .ThenBy(x => x.DistanceMeters)
                    .ToList(),

                "distance" => items
                    .OrderBy(x => x.DistanceMeters)
                    .ThenBy(x => x.EstimatedPrice)
                    .ToList(),

                _ => items
                    .OrderBy(x => x.DistanceMeters)
                    .ToList()
            };

            return items;
        }

    }
}
