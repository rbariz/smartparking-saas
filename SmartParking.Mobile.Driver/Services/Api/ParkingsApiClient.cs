using SmartParking.Contracts.Bookings;
using SmartParking.Contracts.Drivers;
using SmartParking.Contracts.Parkings;
using SmartParking.Contracts.Payments;
using System;
using System.Collections.Generic;
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
            var url =
                    $"/api/parkings/search?latitude={request.Latitude}" +
                    $"&longitude={request.Longitude}" +
                    $"&radiusMeters={request.RadiusMeters}" +
                    $"&startTime={Uri.EscapeDataString(request.StartTime?.ToString("O") ?? string.Empty)}" +
                    $"&endTime={Uri.EscapeDataString(request.EndTime?.ToString("O") ?? string.Empty)}" +
                    $"&openOnly={request.OpenOnly}" +
                    $"&sortBy={Uri.EscapeDataString(request.SortBy ?? string.Empty)}";

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
            var url =
                $"/api/parkings/{parkingId}/quote?startTime={Uri.EscapeDataString(startTime.ToString("O"))}" +
                $"&endTime={Uri.EscapeDataString(endTime.ToString("O"))}";

            return await _httpClient.GetFromJsonAsync<ParkingQuoteResponse>(url, cancellationToken);
        }
    }
}
