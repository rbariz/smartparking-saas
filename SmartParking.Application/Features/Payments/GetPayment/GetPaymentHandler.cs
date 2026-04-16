using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Payments.GetPayment
{
    
    public sealed class GetPaymentHandler
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentResponse> HandleAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId, cancellationToken);
            if (payment is null)
                throw new NotFoundException("Payment not found.");

            return new PaymentResponse(
                payment.Id,
                payment.BookingId,
                payment.Amount,
                payment.Currency,
                payment.Status.ToApiCode(),
                payment.Method,
                payment.ProviderReference,
                payment.CreatedAtUtc,
                payment.PaidAtUtc);
        }
    }
}
