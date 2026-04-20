using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartParking.Mobile.Driver.Services;
using SmartParking.Mobile.Driver.Services.Api;

namespace SmartParking.Mobile.Driver;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        using var stream = FileSystem.OpenAppPackageFileAsync("wwwroot/appsettings.json").GetAwaiter().GetResult();
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(config);

        var apiSettings = new ApiSettings();
        builder.Configuration.GetSection("ApiSettings").Bind(apiSettings);
        builder.Services.AddSingleton(apiSettings);

        builder.Services.AddSingleton<DriverSession>();
        builder.Services.AddSingleton<SearchState>();

        builder.Services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<ApiSettings>();

            return new HttpClient
            {
                BaseAddress = new Uri(settings.BaseUrl)
            };
        });

        builder.Services.AddScoped<ParkingsApiClient>();
        builder.Services.AddScoped<BookingsApiClient>();
        builder.Services.AddScoped<PaymentsApiClient>();
        builder.Services.AddScoped<DriversApiClient>();
        builder.Services.AddScoped<DriverAuthApiService>();
        builder.Services.AddSingleton<LocationService>();
        builder.Services.AddSingleton<DriverPreferencesService>();
        builder.Services.AddSingleton<DriverAuthStateService>();


        var mauiApp = builder.Build();

        var driverSession = mauiApp.Services.GetRequiredService<DriverSession>();
        var driverPreferences = mauiApp.Services.GetRequiredService<DriverPreferencesService>();

        var savedSession = driverPreferences.GetSession();

        if (savedSession is not null && savedSession.DriverId != Guid.Empty)
        {
            // OTP policy:
            // we remember the driver, but we do NOT auto-restore authentication
            driverSession.SetDriver(
                savedSession.DriverId,
                savedSession.DriverName,
                savedSession.Phone,
                savedSession.Email,
                accessToken: null,
                expiresAtUtc: null);
        }
        else
        {
            driverSession.Clear();
        }

        return mauiApp;
    }
}
