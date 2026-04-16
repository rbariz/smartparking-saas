
namespace SmartParking.Api.IntegrationTests.Flows
{

    public sealed class CoreLoopTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        private readonly CustomWebApplicationFactory _factory;

        public CoreLoopTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Booking_on_closed_parking_should_return_409()
        {
            await _factory.ResetDatabaseAsync();

            var operatorResponse = await CreateOperatorAsync();
            var driverResponse = await CreateDriverAsync();
            var parking = await CreateParkingAsync(operatorResponse.Id);

            var start = DateTime.UtcNow.AddMinutes(10);
            var end = start.AddHours(2);

            var response = await _client.PostAsJsonAsync(
                "/api/bookings",
                new BookingCreateRequest(driverResponse.Id, parking.Id, start, end));

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Confirm_unknown_payment_should_return_404()
        {
            await _factory.ResetDatabaseAsync();

            var response = await _client.PostAsJsonAsync(
                $"/api/payments/{Guid.NewGuid()}/confirm",
                new PaymentConfirmRequest("TEST-UNKNOWN"));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            Console.WriteLine($"StatusCode = {(int)response.StatusCode} ({response.StatusCode})");

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Payment_on_confirmed_booking_should_return_409()
        {
            await _factory.ResetDatabaseAsync();

            var op = await CreateOperatorAsync();
            var driver = await CreateDriverAsync();
            var parking = await CreateParkingAsync(op.Id);
            await PublishAvailabilityAsync(parking.Id);

            var start = DateTime.UtcNow.AddMinutes(10);
            var end = start.AddHours(2);

            var booking = await CreateBookingAsync(driver.Id, parking.Id, start, end);

            var payment = await CreatePaymentAsync(booking.Id);
            await ConfirmPaymentAsync(payment.Id);

            //  deuxième payment interdit
            var response = await _client.PostAsJsonAsync(
                $"/api/bookings/{booking.Id}/payments",
                new PaymentCreateRequest("card"));

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);

            Console.WriteLine($"StatusCode = {(int)response.StatusCode} ({response.StatusCode})");

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Booking_without_capacity_should_return_409()
        {
            await _factory.ResetDatabaseAsync();

            var operatorResponse = await CreateOperatorAsync();
            var driver = await CreateDriverAsync();
            var parking = await CreateParkingAsync(operatorResponse.Id);

            await PublishAvailabilityAsync(parking.Id, availableCount: 0);

            var start = DateTime.UtcNow.AddMinutes(10);
            var end = start.AddHours(2);

            var response = await _client.PostAsJsonAsync("/api/bookings",
                new BookingCreateRequest(driver.Id, parking.Id, start, end));

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }


        //[Fact]
        //public async Task Core_loop_should_work_end_to_end()
        //{
        //    // 1. Create operator
        //    var operatorResponse = await CreateOperatorAsync();

        //    // 2. Create driver
        //    var driverResponse = await CreateDriverAsync();

        //    // 3. Create parking
        //    var parkingResponse = await CreateParkingAsync(operatorResponse.Id);

        //    // 4. Publish availability
        //    var availabilityResponse = await PublishAvailabilityAsync(parkingResponse.Id);

        //    // 5. Quote
        //    var startTime = DateTime.UtcNow.AddMinutes(10);
        //    var endTime = startTime.AddHours(2);

        //    var quoteResponse = await _client.GetAsync(
        //        $"/api/parkings/{parkingResponse.Id}/quote?startTime={Uri.EscapeDataString(startTime.ToString("O"))}&endTime={Uri.EscapeDataString(endTime.ToString("O"))}");

        //    quoteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        //    // 6. Create booking
        //    var bookingRequest = new BookingCreateRequest(
        //        driverResponse.Id,
        //        parkingResponse.Id,
        //        startTime,
        //        endTime);

        //    var bookingHttpResponse = await _client.PostAsJsonAsync("/api/bookings", bookingRequest);
        //    bookingHttpResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        //    var booking = await bookingHttpResponse.Content.ReadFromJsonAsync<BookingResponse>();
        //    booking.Should().NotBeNull();
        //    booking!.Status.Should().Be("pending_payment");

        //    // 7. Create payment
        //    var paymentCreateResponse = await _client.PostAsJsonAsync(
        //        $"/api/bookings/{booking.Id}/payments",
        //        new PaymentCreateRequest(TestData.PaymentMethod));

        //    paymentCreateResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        //    var payment = await paymentCreateResponse.Content.ReadFromJsonAsync<PaymentResponse>();
        //    payment.Should().NotBeNull();
        //    payment!.Status.Should().Be("pending");

        //    // 8. Confirm payment
        //    var confirmResponse = await _client.PostAsJsonAsync(
        //        $"/api/payments/{payment.Id}/confirm",
        //        new PaymentConfirmRequest(TestData.ProviderReference));

        //    confirmResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var confirmedPayment = await confirmResponse.Content.ReadFromJsonAsync<PaymentResponse>();
        //    confirmedPayment.Should().NotBeNull();
        //    confirmedPayment!.Status.Should().Be("paid");

        //    // 9. Re-read booking
        //    var bookingGetResponse = await _client.GetAsync($"/api/bookings/{booking.Id}");
        //    bookingGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var confirmedBooking = await bookingGetResponse.Content.ReadFromJsonAsync<BookingResponse>();
        //    confirmedBooking.Should().NotBeNull();
        //    confirmedBooking!.Status.Should().Be("confirmed");
        //}

        private async Task<OperatorResponse> CreateOperatorAsync()
        {
            var response = await _client.PostAsJsonAsync(
                "/api/operators",
                new OperatorCreateRequest(
                    TestData.OperatorName,
                    TestData.OperatorEmail,
                    TestData.OperatorPhone));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var payload = await response.Content.ReadFromJsonAsync<OperatorResponse>();
            payload.Should().NotBeNull();
            return payload!;
        }

        private async Task<DriverResponse> CreateDriverAsync()
        {
            var response = await _client.PostAsJsonAsync(
                "/api/drivers",
                new DriverCreateRequest(
                    TestData.DriverName,
                    TestData.DriverPhone,
                    TestData.DriverEmail));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var payload = await response.Content.ReadFromJsonAsync<DriverResponse>();
            payload.Should().NotBeNull();
            return payload!;
        }

        private async Task<ParkingResponse> CreateParkingAsync(Guid operatorId)
        {
            var response = await _client.PostAsJsonAsync(
                $"/api/operators/{operatorId}/parkings",
                new ParkingCreateRequest(
                    TestData.ParkingName,
                    TestData.ParkingAddress,
                    TestData.ParkingLatitude,
                    TestData.ParkingLongitude,
                    TestData.ParkingCapacity,
                    TestData.Currency,
                    TestData.HourlyRate));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var payload = await response.Content.ReadFromJsonAsync<ParkingResponse>();
            payload.Should().NotBeNull();
            return payload!;
        }

        private async Task<ParkingAvailabilityResponse> PublishAvailabilityAsync(Guid parkingId)
        {
            var response = await _client.PutAsJsonAsync(
                $"/api/parkings/{parkingId}/availability",
                new ParkingAvailabilityUpdateRequest(true, TestData.PublishedAvailableCount));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var payload = await response.Content.ReadFromJsonAsync<ParkingAvailabilityResponse>();
            payload.Should().NotBeNull();
            return payload!;
        }

        private async Task<ParkingAvailabilityResponse> PublishAvailabilityAsync(Guid parkingId, int availableCount = TestData.PublishedAvailableCount)
        {
            var response = await _client.PutAsJsonAsync(
                $"/api/parkings/{parkingId}/availability",
                new ParkingAvailabilityUpdateRequest(true, availableCount));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var payload = await response.Content.ReadFromJsonAsync<ParkingAvailabilityResponse>();
            payload.Should().NotBeNull();
            return payload!;
        }

        private async Task<BookingResponse> CreateBookingAsync(Guid driverId, Guid parkingId, DateTime startTime, DateTime endTime)
        {
            var response = await _client.PostAsJsonAsync(
                "/api/bookings",
                new BookingCreateRequest(driverId, parkingId, startTime, endTime));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var payload = await response.Content.ReadFromJsonAsync<BookingResponse>();
            payload.Should().NotBeNull();
            return payload!;
        }

        private async Task<PaymentResponse> CreatePaymentAsync(Guid bookingId)
        {
            var response = await _client.PostAsJsonAsync(
                $"/api/bookings/{bookingId}/payments",
                new PaymentCreateRequest(TestData.PaymentMethod));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var payload = await response.Content.ReadFromJsonAsync<PaymentResponse>();
            payload.Should().NotBeNull();
            return payload!;
        }

        private async Task<PaymentResponse> ConfirmPaymentAsync(Guid paymentId)
        {
            var response = await _client.PostAsJsonAsync(
                $"/api/payments/{paymentId}/confirm",
                new PaymentConfirmRequest(TestData.ProviderReference));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var payload = await response.Content.ReadFromJsonAsync<PaymentResponse>();
            payload.Should().NotBeNull();
            return payload!;
        }
    }
}
