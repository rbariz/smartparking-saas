using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Parkings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Parkings.GetParkingById
{
   

    public sealed class GetParkingByIdHandler
    {
        private readonly IParkingRepository _parkingRepository;

        public GetParkingByIdHandler(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        public async Task<ParkingResponse> HandleAsync(
            Guid parkingId,
            CancellationToken cancellationToken = default)
        {
            var entity = await _parkingRepository.GetByIdAsync(parkingId, cancellationToken);
            if (entity is null)
                throw new NotFoundException("Parking not found.");

            return new ParkingResponse(
                entity.Id,
                entity.OperatorId,
                entity.Name,
                entity.Address,
                entity.Latitude,
                entity.Longitude,
                entity.TotalCapacity,
                entity.AvailableCount,
                entity.Status.ToApiCode(),
                entity.Currency,
                entity.HourlyRate,
                entity.CreatedAtUtc,
                entity.UpdatedAtUtc);
        }
    }
}
