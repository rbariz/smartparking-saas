using SmartParking.Domain.Entities;

namespace SmartParking.Application.Abstractions.Persistence
{
    public interface IOperatorRepository
    {
        Task AddAsync(Operator entity, CancellationToken cancellationToken = default);
        Task<Operator?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
