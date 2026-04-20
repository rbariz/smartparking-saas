using SmartParking.Contracts.Bookings;
using SmartParking.Contracts.DriverAuth;
using System.Net.Http.Json;
using System.Text.Json;

namespace SmartParking.Mobile.Driver.Services.Api
{
    public sealed class BookingsApiClient
    {
        private readonly HttpClient _httpClient;

        public BookingsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BookingResponse?> CreateAsync(
            BookingCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/bookings", request, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BookingResponse>(cancellationToken: cancellationToken);
        }

        public async Task<BookingResponse?> GetByIdAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<BookingResponse>($"/api/bookings/{bookingId}", cancellationToken);
        }

        public async Task<IReadOnlyList<DriverBookingListItemResponse>> GetDriverBookingsAsync(
            Guid driverId,
            CancellationToken cancellationToken = default)
        {
            var result = await _httpClient.GetFromJsonAsync<List<DriverBookingListItemResponse>>(
                $"/api/drivers/{driverId}/bookings",
                cancellationToken);

            return result ?? [];
        }
    }
}
