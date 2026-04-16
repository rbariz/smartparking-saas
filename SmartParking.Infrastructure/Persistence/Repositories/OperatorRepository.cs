using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence.Repositories
{
    public sealed class OperatorRepository : IOperatorRepository
    {
        private readonly SmartParkingDbContext _dbContext;

        public OperatorRepository(SmartParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Operator entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Operators.AddAsync(entity, cancellationToken);
        }

        public Task<Operator?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Operators.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
