using SmartParking.Application.Abstractions.Services;

namespace SmartParking.Api.BackgroundServices
{
    public sealed class BookingExpirationBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<BookingExpirationBackgroundService> _logger;

        public BookingExpirationBackgroundService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<BookingExpirationBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();

                    var expirationService = scope.ServiceProvider
                        .GetRequiredService<IBookingExpirationService>();

                    var expiredCount = await expirationService
                        .ExpirePendingPaymentBookingsAsync(stoppingToken);

                    if (expiredCount > 0)
                    {
                        _logger.LogInformation(
                            "Expired {ExpiredCount} pending payment booking(s).",
                            expiredCount);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while expiring pending payment bookings.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
