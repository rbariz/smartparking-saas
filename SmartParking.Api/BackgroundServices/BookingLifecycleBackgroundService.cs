using SmartParking.Application.Abstractions.Services;

namespace SmartParking.Api.BackgroundServices
{
    public sealed class BookingLifecycleBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<BookingLifecycleBackgroundService> _logger;

        public BookingLifecycleBackgroundService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<BookingLifecycleBackgroundService> logger)
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

                    var lifecycleService = scope.ServiceProvider
                        .GetRequiredService<IBookingLifecycleService>();

                    var result = await lifecycleService.ProcessLifecycleAsync(stoppingToken);

                    if (result.activated > 0 || result.completed > 0)
                    {
                        _logger.LogInformation(
                            "Booking lifecycle processed. Activated={Activated}, Completed={Completed}",
                            result.activated,
                            result.completed);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while processing booking lifecycle.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
