using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;

namespace SmartParking.Application.Abstractions.Persistence
{
    public interface IDriverOtpChallengeRepository
    {
        Task<DriverOtpChallenge?> GetLatestActiveAsync(
            string contact,
            OtpPurpose purpose,
            CancellationToken cancellationToken = default);

        Task AddAsync(DriverOtpChallenge challenge, CancellationToken cancellationToken = default);
    }
}
