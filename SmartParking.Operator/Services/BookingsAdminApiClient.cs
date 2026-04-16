using SmartParking.Contracts.Bookings;

namespace SmartParking.Operator.Services
{
    public sealed class BookingsAdminApiClient
    {
        private readonly HttpClient _httpClient;

        public BookingsAdminApiClient(IHttpClientFactory factory, ApiSettings settings)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri(settings.BaseUrl);
        }

        public async Task<IReadOnlyList<BookingResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _httpClient.GetFromJsonAsync<List<BookingResponse>>(
                "/api/bookings",
                cancellationToken);

            return result ?? [];
        }

        public async Task<BookingResponse?> GetByIdAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<BookingResponse>(
                $"/api/bookings/{bookingId}",
                cancellationToken);
        }
        public async Task CancelAsync(Guid bookingId)
        {
            var res = await _httpClient.PostAsync($"/api/bookings/{bookingId}/cancel", null);
            res.EnsureSuccessStatusCode();
        }

        public async Task CompleteAsync(Guid bookingId)
        {
            var res = await _httpClient.PostAsync($"/api/bookings/{bookingId}/complete", null);
            res.EnsureSuccessStatusCode();
        }

        public async Task ExpireAsync(Guid bookingId)
        {
            var res = await _httpClient.PostAsync($"/api/bookings/{bookingId}/expire", null);
            res.EnsureSuccessStatusCode();
        }
    }
}
