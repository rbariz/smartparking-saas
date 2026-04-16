using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Parkings.GetAllPayments
{
    public sealed class GetAllPaymentsHandler
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetAllPaymentsHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IReadOnlyList<PaymentResponse>> HandleAsync(
            CancellationToken cancellationToken = default)
        {
            var items = await _paymentRepository.GetAllAsync(cancellationToken);

            return items
                .OrderByDescending(x => x.CreatedAtUtc)
                .Select(x => new PaymentResponse(
                    x.Id,
                    x.BookingId,
                    x.Amount,                         // ✔ OK
                    x.Currency,                       // ✔ OK
                    x.Status.ToApiCode(),             // ✔ OK
                    x.Method,                         // ✔ OK
                    x.ProviderReference,              // ✔ OK
                    x.CreatedAtUtc,                   // ✔ OK
                    x.PaidAtUtc                       // ✔ IMPORTANT
                ))
                .ToList();
        }
    }

}
