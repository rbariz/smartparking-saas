using SmartParking.Contracts.Bookings;
using SmartParking.Contracts.Drivers;
using SmartParking.Contracts.Parkings;
using SmartParking.Contracts.Payments;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Mobile.Driver.Services.Api
{
    
    public sealed class ParkingsApiClient
    {
        private readonly HttpClient _httpClient;

        public ParkingsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<ParkingSearchItemResponse>> SearchAsync(
            ParkingSearchRequest request,
            CancellationToken cancellationToken = default)
        {
            var latitude = request.Latitude.ToString(CultureInfo.InvariantCulture);
            var longitude = request.Longitude.ToString(CultureInfo.InvariantCulture);
            var radiusMeters = request.RadiusMeters.ToString(CultureInfo.InvariantCulture);
            var startTime = Uri.EscapeDataString(
                request.StartTime?.ToString("O", CultureInfo.InvariantCulture) ?? string.Empty);
            var endTime = Uri.EscapeDataString(
                request.EndTime?.ToString("O", CultureInfo.InvariantCulture) ?? string.Empty);
            var openOnly = request.OpenOnly.ToString().ToLowerInvariant();
            var sortBy = Uri.EscapeDataString(request.SortBy ?? string.Empty);

            var url =
                $"/api/parkings/search?latitude={latitude}" +
                $"&longitude={longitude}" +
                $"&radiusMeters={radiusMeters}" +
                $"&startTime={startTime}" +
                $"&endTime={endTime}" +
                $"&openOnly={openOnly}" +
                $"&sortBy={sortBy}";

            var result = await _httpClient.GetFromJsonAsync<List<ParkingSearchItemResponse>>(url, cancellationToken);
            return result ?? [];
        }

        public async Task<ParkingResponse?> GetByIdAsync(Guid parkingId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ParkingResponse>($"/api/parkings/{parkingId}", cancellationToken);
        }

        public async Task<ParkingQuoteResponse?> GetQuoteAsync(
            Guid parkingId,
            DateTime startTime,
            DateTime endTime,
            CancellationToken cancellationToken = default)
        {
            var start = Uri.EscapeDataString(startTime.ToString("O", CultureInfo.InvariantCulture));
            var end = Uri.EscapeDataString(endTime.ToString("O", CultureInfo.InvariantCulture));

            var url =
                $"/api/parkings/{parkingId}/quote?startTime={start}" +
                $"&endTime={end}";

            return await _httpClient.GetFromJsonAsync<ParkingQuoteResponse>(url, cancellationToken);
        }
    }
}
