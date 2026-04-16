using SmartParking.Contracts.Payments;

namespace SmartParking.Application.Abstractions.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> CreateAsync(Guid bookingId, PaymentCreateRequest request, CancellationToken cancellationToken = default);
        Task<PaymentResponse> ConfirmAsync(Guid paymentId, PaymentConfirmRequest request, CancellationToken cancellationToken = default);
        Task<PaymentResponse?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default);
    }
}
