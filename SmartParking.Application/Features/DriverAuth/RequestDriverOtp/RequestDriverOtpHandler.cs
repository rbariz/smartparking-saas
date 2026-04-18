using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Time;
using SmartParking.Contracts.DriverAuth;
using SmartParking.Domain.Common;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.DriverAuth.RequestDriverOtp
{

    

    public sealed class RequestDriverOtpHandler
    {
        private readonly IDriverOtpChallengeRepository _challengeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClock _clock;

        public RequestDriverOtpHandler(
            IDriverOtpChallengeRepository challengeRepository,
            IUnitOfWork unitOfWork,
            IClock clock)
        {
            _challengeRepository = challengeRepository;
            _unitOfWork = unitOfWork;
            _clock = clock;
        }

        public async Task<RequestDriverOtpResponse> HandleAsync(
            RequestDriverOtpRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Contact))
                throw new DomainException("Contact is required.");

            if (!Enum.TryParse<OtpChannel>(request.Channel, true, out var channel))
                throw new DomainException("Invalid OTP channel.");

            var normalizedContact = request.Contact.Trim();
            var nowUtc = _clock.UtcNow;

            var code = GenerateOtpCode();

            var challenge = new DriverOtpChallenge(
                normalizedContact,
                channel,
                OtpPurpose.Login,
                code,
                nowUtc,
                nowUtc.AddMinutes(5),
                maxAttempts: 5);

            await _challengeRepository.AddAsync(challenge, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // v1 demo: on retourne le code dans le message pour tester rapidement
            return new RequestDriverOtpResponse(
                true,
                $"OTP generated for demo. Code: {code}",
                300);
        }

        private static string GenerateOtpCode()
        {
            return RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        }
    }
}
