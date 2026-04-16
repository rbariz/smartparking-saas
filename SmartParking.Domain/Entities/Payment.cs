using SmartParking.Domain.Common;
using SmartParking.Domain.Enums;

namespace SmartParking.Domain.Entities
{
    public sealed class Payment : Entity
    {
        public Guid BookingId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = default!;
        public PaymentStatus Status { get; private set; }
        public string Method { get; private set; } = default!;
        public string? ProviderReference { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? PaidAtUtc { get; private set; }

        private Payment()
        {
        }

        public Payment(Guid bookingId, decimal amount, string currency, string method)
        {
            Id = Guid.NewGuid();
            BookingId = bookingId;
            Amount = amount;
            Currency = currency;
            Method = method;
            Status = PaymentStatus.Pending;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public void MarkPaid(string providerReference)
        {
            if (Status != PaymentStatus.Pending)
                throw new DomainException("Only pending payments can be marked as paid.");

            Status = PaymentStatus.Paid;
            ProviderReference = providerReference;
            PaidAtUtc = DateTime.UtcNow;
        }

        public void MarkFailed(string? providerReference = null)
        {
            if (Status != PaymentStatus.Pending)
                throw new DomainException("Only pending payments can be marked as failed.");

            Status = PaymentStatus.Failed;
            ProviderReference = providerReference;
        }
    }

}
