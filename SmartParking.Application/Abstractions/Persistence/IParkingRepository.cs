using SmartParking.Domain.Entities;

namespace SmartParking.Application.Abstractions.Persistence
{
    public interface IParkingRepository
    {
        Task AddAsync(Parking entity, CancellationToken cancellationToken = default);
        Task<Parking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Parking>> SearchNearbyAsync(
                    decimal latitude,
                    decimal longitude,
                    int radiusMeters,
                    bool openOnly,
                    CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Parking>> GetAllAsync(
    CancellationToken cancellationToken = default);
    }
}
