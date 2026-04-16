using SmartParking.Contracts.Payments;
using System.Net.Http.Json;

namespace SmartParking.Mobile.Driver.Services.Api
{
    public sealed class PaymentsApiClient
    {
        private readonly HttpClient _httpClient;

        public PaymentsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaymentResponse?> CreateAsync(
            Guid bookingId,
            PaymentCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"/api/bookings/{bookingId}/payments",
                request,
                cancellationToken);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PaymentResponse>(cancellationToken: cancellationToken);
        }

        public async Task<PaymentResponse?> ConfirmAsync(
            Guid paymentId,
            PaymentConfirmRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"/api/payments/{paymentId}/confirm",
                request,
                cancellationToken);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PaymentResponse>(cancellationToken: cancellationToken);
        }

        public async Task<PaymentResponse?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<PaymentResponse>($"/api/payments/{paymentId}", cancellationToken);
        }
    }
}
