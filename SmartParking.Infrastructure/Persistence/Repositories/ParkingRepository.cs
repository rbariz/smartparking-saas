using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;

namespace SmartParking.Infrastructure.Persistence.Repositories
{
    public sealed class ParkingRepository : IParkingRepository
    {
        private readonly SmartParkingDbContext _dbContext;

        public ParkingRepository(SmartParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Parking entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Parkings.AddAsync(entity, cancellationToken);
        }

        public Task<Parking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Parkings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Parking>> SearchNearbyAsync(
    decimal latitude,
    decimal longitude,
    int radiusMeters,
    bool openOnly,
    CancellationToken cancellationToken = default)
        {
            IQueryable<Parking> query = _dbContext.Parkings;

            if (openOnly)
            {
                query = query.Where(x => x.Status == ParkingStatus.Open);
            }

            // query = query.Where(x => x.AvailableCount > 0); tous les parkings

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Parking>> GetAllAsync(
    CancellationToken cancellationToken = default)
        {
            return await _dbContext.Parkings
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
