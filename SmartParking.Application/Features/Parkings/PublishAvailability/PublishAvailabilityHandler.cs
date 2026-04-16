using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Parkings;
using SmartParking.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Parkings.PublishAvailability
{
  

    public sealed class PublishAvailabilityHandler
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublishAvailabilityHandler(
            IParkingRepository parkingRepository,
            IUnitOfWork unitOfWork)
        {
            _parkingRepository = parkingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ParkingAvailabilityResponse> HandleAsync(
            Guid parkingId,
            ParkingAvailabilityUpdateRequest request,
            CancellationToken cancellationToken = default)
        {
            var parking = await _parkingRepository.GetByIdAsync(parkingId, cancellationToken);
            if (parking is null)
                throw new NotFoundException("Parking not found.");

            if (request.IsOpen)
            {
                parking.Open(request.AvailableCount);
            }
            else
            {
                parking.Close();
                parking.UpdateAvailability(request.AvailableCount);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var status = parking.AvailableCount == 0
                        ? "full"
                        : parking.Status.ToApiCode();

            return new ParkingAvailabilityResponse(
                parking.Id,
                status,
                parking.TotalCapacity,
                parking.AvailableCount,
                parking.UpdatedAtUtc);
        }
    }
}
