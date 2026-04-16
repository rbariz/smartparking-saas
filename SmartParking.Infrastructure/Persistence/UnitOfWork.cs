using SmartParking.Application.Abstractions.Persistence;

namespace SmartParking.Infrastructure.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly SmartParkingDbContext _dbContext;

        public UnitOfWork(SmartParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }


}
