using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Contracts.Drivers;
using SmartParking.Domain.Entities;

namespace SmartParking.Application.Features.Drivers.CreateDriver
{
    public sealed class CreateDriverHandler
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDriverHandler(
            IDriverRepository driverRepository,
            IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DriverResponse> HandleAsync(
            DriverCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            var entity = new Driver(
                request.FullName,
                request.Phone,
                request.Email);

            await _driverRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DriverResponse(
                entity.Id,
                entity.FullName,
                entity.Phone,
                entity.Email,
                entity.CreatedAtUtc);
        }
    }
}
