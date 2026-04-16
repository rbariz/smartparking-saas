using SmartParking.Contracts.Drivers;
using System.Net.Http.Json;

namespace SmartParking.Mobile.Driver.Services.Api
{
    public sealed class DriversApiClient
    {
        private readonly HttpClient _httpClient;

        public DriversApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DriverResponse?> GetByIdAsync(Guid driverId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<DriverResponse>($"/api/drivers/{driverId}", cancellationToken);
        }
    }
}
