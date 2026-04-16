using SmartParking.Contracts.Payments;

namespace SmartParking.Operator.Services
{
    public sealed class PaymentsAdminApiClient
    {
        private readonly HttpClient _httpClient;

        public PaymentsAdminApiClient(IHttpClientFactory factory, ApiSettings settings)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri(settings.BaseUrl);
        }

        public async Task<IReadOnlyList<PaymentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _httpClient.GetFromJsonAsync<List<PaymentResponse>>(
                "/api/payments",
                cancellationToken);

            return result ?? [];
        }

        public async Task<PaymentResponse?> GetByIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<PaymentResponse>(
                $"/api/payments/{paymentId}",
                cancellationToken);
        }
    }
}
