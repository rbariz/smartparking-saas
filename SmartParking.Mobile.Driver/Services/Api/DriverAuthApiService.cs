using SmartParking.Contracts.DriverAuth;
using System.Net.Http.Json;

namespace SmartParking.Mobile.Driver.Services.Api
{
    public sealed class DriverAuthApiService
    {
        private readonly HttpClient _httpClient;

        public DriverAuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestDriverOtpResponse> RequestOtpAsync(
            RequestDriverOtpRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/driver-auth/request-otp", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<RequestDriverOtpResponse>(cancellationToken: cancellationToken))!;
        }

        public async Task<DriverAuthResponse> VerifyOtpAsync(
            VerifyDriverOtpRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/driver-auth/verify-otp", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<DriverAuthResponse>(cancellationToken: cancellationToken))!;
        }
    }
}
