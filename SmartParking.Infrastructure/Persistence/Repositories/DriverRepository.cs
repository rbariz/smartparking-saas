using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence.Repositories
{
    public sealed class DriverRepository : IDriverRepository
    {
        private readonly SmartParkingDbContext _dbContext;

        public DriverRepository(SmartParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Driver entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Drivers.AddAsync(entity, cancellationToken);
        }

        

        public Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Driver?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Drivers
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<Driver?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Drivers
                .FirstOrDefaultAsync(x => x.Phone == phone, cancellationToken);
        }
    }
}
