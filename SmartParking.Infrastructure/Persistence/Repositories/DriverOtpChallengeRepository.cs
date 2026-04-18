using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;

namespace SmartParking.Infrastructure.Persistence.Repositories
{
    public sealed class DriverOtpChallengeRepository : IDriverOtpChallengeRepository
    {
        private readonly SmartParkingDbContext _dbContext;

        public DriverOtpChallengeRepository(SmartParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(DriverOtpChallenge challenge, CancellationToken cancellationToken = default)
        {
            await _dbContext.DriverOtpChallenges.AddAsync(challenge, cancellationToken);
        }

        public async Task<DriverOtpChallenge?> GetLatestActiveAsync(
            string contact,
            OtpPurpose purpose,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.DriverOtpChallenges
                .Where(x => x.Contact == contact && x.Purpose == purpose)
                .OrderByDescending(x => x.CreatedAtUtc)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
