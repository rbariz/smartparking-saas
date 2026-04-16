using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Contracts.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Drivers.GetDriverById
{


    public sealed class GetDriverByIdHandler
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriverByIdHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<DriverResponse> HandleAsync(
            Guid driverId,
            CancellationToken cancellationToken = default)
        {
            var entity = await _driverRepository.GetByIdAsync(driverId, cancellationToken);
            if (entity is null)
                throw new NotFoundException("Driver not found.");

            return new DriverResponse(
                entity.Id,
                entity.FullName,
                entity.Phone,
                entity.Email,
                entity.CreatedAtUtc);
        }
    }
}
