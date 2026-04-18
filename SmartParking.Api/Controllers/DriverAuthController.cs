using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.DriverAuth.RequestDriverOtp;
using SmartParking.Application.Features.DriverAuth.VerifyDriverOtp;
using SmartParking.Contracts.DriverAuth;

namespace SmartParking.Api.Controllers
{
    [ApiController]
    [Route("api/driver-auth")]
    public sealed class DriverAuthController : ControllerBase
    {
        private readonly RequestDriverOtpHandler _requestDriverOtpHandler;
        private readonly VerifyDriverOtpHandler _verifyDriverOtpHandler;

        public DriverAuthController(
            RequestDriverOtpHandler requestDriverOtpHandler,
            VerifyDriverOtpHandler verifyDriverOtpHandler)
        {
            _requestDriverOtpHandler = requestDriverOtpHandler;
            _verifyDriverOtpHandler = verifyDriverOtpHandler;
        }

        [HttpPost("request-otp")]
        [ProducesResponseType(typeof(RequestDriverOtpResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RequestOtp(
            [FromBody] RequestDriverOtpRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _requestDriverOtpHandler.HandleAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("verify-otp")]
        [ProducesResponseType(typeof(DriverAuthResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyOtp(
            [FromBody] VerifyDriverOtpRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _verifyDriverOtpHandler.HandleAsync(request, cancellationToken);
            return Ok(result);
        }
    }
}
