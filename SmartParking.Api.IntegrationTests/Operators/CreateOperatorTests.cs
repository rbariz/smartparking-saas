

namespace SmartParking.Api.IntegrationTests.Operators
{
    public sealed class CreateOperatorTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CreateOperatorTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_operators_should_create_operator()
        {
            var request = new OperatorCreateRequest(
                TestData.OperatorName,
                TestData.OperatorEmail,
                TestData.OperatorPhone);

            var response = await _client.PostAsJsonAsync("/api/operators", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var payload = await response.Content.ReadFromJsonAsync<OperatorResponse>();
            payload.Should().NotBeNull();
            payload!.Name.Should().Be(TestData.OperatorName);
            payload.ContactEmail.Should().Be(TestData.OperatorEmail);
            payload.ContactPhone.Should().Be(TestData.OperatorPhone);
            payload.Id.Should().NotBe(Guid.Empty);
        }
    }
}
