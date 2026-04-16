namespace SmartParking.Contracts.Payments
{
    public sealed record PaymentResponse(
    Guid Id,
    Guid BookingId,
    decimal Amount,
    string Currency,
    string Status,
    string Method,
    string? ProviderReference,
    DateTime CreatedAtUtc,
    DateTime? PaidAtUtc
);
}
