using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Parkings;
using SmartParking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Parkings.CreateParking
{
    
    public sealed class CreateParkingHandler
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IParkingRepository _parkingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateParkingHandler(
            IOperatorRepository operatorRepository,
            IParkingRepository parkingRepository,
            IUnitOfWork unitOfWork)
        {
            _operatorRepository = operatorRepository;
            _parkingRepository = parkingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ParkingResponse> HandleAsync(
            Guid operatorId,
            ParkingCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            var operatorEntity = await _operatorRepository.GetByIdAsync(operatorId, cancellationToken);
            if (operatorEntity is null)
                throw new NotFoundException("Operator not found.");

            var entity = new Parking(
                operatorId,
                request.Name,
                request.Address,
                request.Latitude,
                request.Longitude,
                request.TotalCapacity,
                request.Currency,
                request.HourlyRate);

            await _parkingRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
