namespace SmartParking.Application.Abstractions.Services
{
    public interface IBookingExpirationService
    {
        Task<int> ExpirePendingPaymentBookingsAsync(CancellationToken cancellationToken = default);
    }
}
