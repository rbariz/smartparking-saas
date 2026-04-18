using SmartParking.Domain.Entities;

namespace SmartParking.Application.Abstractions.Persistence
{
    public interface IDriverRepository
    {
        Task AddAsync(Driver entity, CancellationToken cancellationToken = default);
        Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Driver?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default);
        Task<Driver?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    }
}
