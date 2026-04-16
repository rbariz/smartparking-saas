using SmartParking.Contracts.Parkings;

namespace SmartParking.Operator.Services
{
    public sealed class ParkingsAdminApiClient
    {
        private readonly HttpClient _httpClient;

        public ParkingsAdminApiClient(IHttpClientFactory factory, ApiSettings settings)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri(settings.BaseUrl);
        }

        public async Task<IReadOnlyList<ParkingResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ParkingResponse>>(
                "/api/parkings",
                cancellationToken);

            return result ?? [];
        }

        public async Task<ParkingResponse?> GetByIdAsync(Guid parkingId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ParkingResponse>(
                $"/api/parkings/{parkingId}",
                cancellationToken);
        }

        public async Task<ParkingAvailabilityResponse?> UpdateAvailabilityAsync(
    Guid parkingId,
    ParkingAvailabilityUpdateRequest request,
    CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"/api/parkings/{parkingId}/availability",
                request,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ParkingAvailabilityResponse>(
                cancellationToken: cancellationToken);
        }
    }
}
