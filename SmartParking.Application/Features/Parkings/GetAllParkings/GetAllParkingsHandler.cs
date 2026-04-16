using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Parkings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Parkings.GetAllParkings
{


    public sealed class GetAllParkingsHandler
    {
        private readonly IParkingRepository _parkingRepository;

        public GetAllParkingsHandler(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        public async Task<IReadOnlyList<ParkingResponse>> HandleAsync(
            CancellationToken cancellationToken = default)
        {
            var items = await _parkingRepository.GetAllAsync(cancellationToken);

            return items
                .OrderBy(x => x.Name)
                .Select(x => new ParkingResponse(
                    x.Id,
                    x.OperatorId,
                    x.Name,
                    x.Address,
                    x.Latitude,
                    x.Longitude,
                    x.TotalCapacity,
                    x.AvailableCount,
                    x.Status.ToApiCode(),
                    x.Currency,
                    x.HourlyRate,
                    x.CreatedAtUtc,
                    x.UpdatedAtUtc))
                .ToList();
        }
    }
}
