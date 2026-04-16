using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.Bookings.Admin.CancelBooking;
using SmartParking.Application.Features.Bookings.Admin.CompleteBooking;
using SmartParking.Application.Features.Bookings.Admin.ExpireBooking;
using SmartParking.Application.Features.Bookings.CreateBooking;
using SmartParking.Application.Features.Bookings.GetAllBookings;
using SmartParking.Application.Features.Bookings.GetBooking;
using SmartParking.Application.Features.Bookings.ListDriverBookings;
using SmartParking.Contracts.Bookings;

namespace SmartParking.Api.Controllers
{
    

    [ApiController]
    [Route("api")]
    public sealed class BookingsController : ControllerBase
    {
        private readonly CreateBookingHandler _createBookingHandler;
        private readonly GetBookingHandler _getBookingHandler;
        private readonly ListDriverBookingsHandler _listDriverBookingsHandler;
        private readonly GetAllBookingsHandler _getAllBookingsHandler;
        private readonly CancelBookingHandler _cancelBookingHandler;
        private readonly CompleteBookingHandler _completeBookingHandler;
        private readonly ExpireBookingHandler _expireBookingHandler;

        

        public BookingsController(
            CreateBookingHandler createBookingHandler,
            GetBookingHandler getBookingHandler,
            ListDriverBookingsHandler listDriverBookingsHandler,
            GetAllBookingsHandler getAllBookingsHandler,
            CancelBookingHandler cancelBookingHandler,
            CompleteBookingHandler completeBookingHandler,
            ExpireBookingHandler expireBookingHandler)
        {
            _createBookingHandler = createBookingHandler;
            _getBookingHandler = getBookingHandler;
            _listDriverBookingsHandler = listDriverBookingsHandler;
            _getAllBookingsHandler = getAllBookingsHandler;
            _cancelBookingHandler = cancelBookingHandler;
            _completeBookingHandler = completeBookingHandler;
            _expireBookingHandler = expireBookingHandler;
        }

        [HttpPost("bookings")]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] BookingCreateRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _createBookingHandler.HandleAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { bookingId = result.Id }, result);
        }

        [HttpGet("bookings/{bookingId:guid}")]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            Guid bookingId,
            CancellationToken cancellationToken)
        {
            var result = await _getBookingHandler.HandleAsync(bookingId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("drivers/{driverId:guid}/bookings")]
        [ProducesResponseType(typeof(IReadOnlyList<DriverBookingListItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDriverBookings(
            Guid driverId,
            CancellationToken cancellationToken)
        {
            var result = await _listDriverBookingsHandler.HandleAsync(driverId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("bookings")]
        [ProducesResponseType(typeof(IReadOnlyList<BookingResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getAllBookingsHandler.HandleAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost("bookings/{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
        {
            await _cancelBookingHandler.HandleAsync(id, ct);
            return NoContent();
        }

        [HttpPost("bookings/{id:guid}/complete")]
        public async Task<IActionResult> Complete(Guid id, CancellationToken ct)
        {
            await _completeBookingHandler.HandleAsync(id, ct);
            return NoContent();
        }

        [HttpPost("bookings/{id:guid}/expire")]
        public async Task<IActionResult> Expire(Guid id, CancellationToken ct)
        {
            await _expireBookingHandler.HandleAsync(id, ct);
            return NoContent();
        }
    }
}
