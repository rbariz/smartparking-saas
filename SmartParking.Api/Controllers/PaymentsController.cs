using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.Parkings.GetAllPayments;
using SmartParking.Application.Features.Payments.ConfirmPayment;
using SmartParking.Application.Features.Payments.CreatePayment;
using SmartParking.Application.Features.Payments.GetPayment;
using SmartParking.Contracts.Payments;

namespace SmartParking.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public sealed class PaymentsController : ControllerBase
    {
        private readonly CreatePaymentHandler _createPaymentHandler;
        private readonly ConfirmPaymentHandler _confirmPaymentHandler;
        private readonly GetPaymentHandler _getPaymentHandler;
        private readonly GetAllPaymentsHandler _getAllPaymentsHandler;

        public PaymentsController(
            CreatePaymentHandler createPaymentHandler,
            ConfirmPaymentHandler confirmPaymentHandler,
            GetPaymentHandler getPaymentHandler,
            GetAllPaymentsHandler getAllPaymentsHandler)
        {
            _createPaymentHandler = createPaymentHandler;
            _confirmPaymentHandler = confirmPaymentHandler;
            _getPaymentHandler = getPaymentHandler;
            _getAllPaymentsHandler = getAllPaymentsHandler;
        }

        [HttpPost("bookings/{bookingId:guid}/payments")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            Guid bookingId,
            [FromBody] PaymentCreateRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _createPaymentHandler.HandleAsync(bookingId, request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { paymentId = result.Id }, result);
        }

        [HttpPost("payments/{paymentId:guid}/confirm")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Confirm(
            Guid paymentId,
            [FromBody] PaymentConfirmRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _confirmPaymentHandler.HandleAsync(paymentId, request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("payments/{paymentId:guid}")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            Guid paymentId,
            CancellationToken cancellationToken)
        {
            var result = await _getPaymentHandler.HandleAsync(paymentId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("payments")]
        [ProducesResponseType(typeof(IReadOnlyList<PaymentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getAllPaymentsHandler.HandleAsync(cancellationToken);
            return Ok(result);
        }
    }
}
