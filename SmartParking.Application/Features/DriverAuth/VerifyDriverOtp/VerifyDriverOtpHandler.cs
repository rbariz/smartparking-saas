using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Time;
using SmartParking.Contracts.DriverAuth;
using SmartParking.Domain.Common;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.DriverAuth.VerifyDriverOtp
{

    public sealed class VerifyDriverOtpHandler
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IDriverOtpChallengeRepository _challengeRepository;
        private readonly IDriverTokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClock _clock;

        public VerifyDriverOtpHandler(
            IDriverRepository driverRepository,
            IDriverOtpChallengeRepository challengeRepository,
            IDriverTokenService tokenService,
            IUnitOfWork unitOfWork,
            IClock clock)
        {
            _driverRepository = driverRepository;
            _challengeRepository = challengeRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _clock = clock;
        }

        public async Task<DriverAuthResponse> HandleAsync(
            VerifyDriverOtpRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Contact))
                throw new DomainException("Contact is required.");

            if (string.IsNullOrWhiteSpace(request.Code))
                throw new DomainException("OTP code is required.");

            var normalizedContact = request.Contact.Trim();
            var nowUtc = _clock.UtcNow;

            var challenge = await _challengeRepository.GetLatestActiveAsync(
                normalizedContact,
                OtpPurpose.Login,
                cancellationToken);

            if (challenge is null)
                throw new DomainException("OTP challenge not found.");

            var isValid = challenge.VerifyCode(request.Code, nowUtc);
            if (!isValid)
                throw new DomainException("Invalid or expired OTP code.");

            var driver = await _driverRepository.GetByPhoneAsync(normalizedContact, cancellationToken);

            if (driver is null)
            {
                var fullName = string.IsNullOrWhiteSpace(request.FullName)
                    ? "Driver"
                    : request.FullName.Trim();

                driver = new Driver(fullName, normalizedContact, email: null);
                await _driverRepository.AddAsync(driver, cancellationToken);
            }

            driver.MarkLogin(nowUtc);

            var tokenResult = _tokenService.Generate(driver);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DriverAuthResponse(
                tokenResult.AccessToken,
                tokenResult.ExpiresAtUtc,
                new DriverProfileResponse(
                    driver.Id,
                    driver.FullName,
                    driver.Phone,
                    driver.Email));
        }
    }
}
