namespace SmartParking.Application.Abstractions.Services
{
    public interface IBookingLifecycleService
    {
        Task<(int activated, int completed)> ProcessLifecycleAsync(
            CancellationToken cancellationToken = default);
    }
}
