using SmartParking.Domain.Common;
using SmartParking.Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace SmartParking.Domain.Entities
{
   

    public sealed class DriverOtpChallenge : Entity
    {
        public string Contact { get; private set; } = default!;
        public OtpChannel Channel { get; private set; }
        public OtpPurpose Purpose { get; private set; }
        public string CodeHash { get; private set; } = default!;
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime ExpiresAtUtc { get; private set; }
        public DateTime? ConsumedAtUtc { get; private set; }
        public DateTime LastSentAtUtc { get; private set; }
        public int AttemptCount { get; private set; }
        public int MaxAttempts { get; private set; }
        public bool IsBlocked { get; private set; }

        public DriverOtpChallenge()
        {
        }

        public DriverOtpChallenge(
            string contact,
            OtpChannel channel,
            OtpPurpose purpose,
            string rawCode,
            DateTime createdAtUtc,
            DateTime expiresAtUtc,
            int maxAttempts)
        {
            if (string.IsNullOrWhiteSpace(contact))
                throw new DomainException("Contact is required.");

            if (string.IsNullOrWhiteSpace(rawCode))
                throw new DomainException("OTP code is required.");

            if (expiresAtUtc <= createdAtUtc)
                throw new DomainException("Expiration must be after creation.");

            if (maxAttempts <= 0)
                throw new DomainException("Max attempts must be greater than zero.");

            Id = Guid.NewGuid();
            Contact = contact.Trim();
            Channel = channel;
            Purpose = purpose;
            CodeHash = ComputeSha256(rawCode.Trim());
            CreatedAtUtc = createdAtUtc;
            ExpiresAtUtc = expiresAtUtc;
            LastSentAtUtc = createdAtUtc;
            MaxAttempts = maxAttempts;
            AttemptCount = 0;
            IsBlocked = false;
        }

        public bool IsExpired(DateTime nowUtc) => nowUtc > ExpiresAtUtc;

        public bool IsConsumed() => ConsumedAtUtc.HasValue;

        public bool CanBeVerified(DateTime nowUtc)
        {
            return !IsBlocked && !IsConsumed() && !IsExpired(nowUtc) && AttemptCount < MaxAttempts;
        }

        public bool VerifyCode(string rawCode, DateTime nowUtc)
        {
            if (string.IsNullOrWhiteSpace(rawCode))
                return false;

            if (!CanBeVerified(nowUtc))
                return false;

            var hash = ComputeSha256(rawCode.Trim());
            var success = string.Equals(hash, CodeHash, StringComparison.Ordinal);

            if (success)
            {
                ConsumedAtUtc = nowUtc;
                return true;
            }

            AttemptCount++;

            if (AttemptCount >= MaxAttempts)
                IsBlocked = true;

            return false;
        }

        public void MarkResent(DateTime nowUtc)
        {
            if (IsBlocked)
                throw new DomainException("Blocked OTP challenge cannot be resent.");

            if (IsConsumed())
                throw new DomainException("Consumed OTP challenge cannot be resent.");

            LastSentAtUtc = nowUtc;
        }

        private static string ComputeSha256(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }

}
