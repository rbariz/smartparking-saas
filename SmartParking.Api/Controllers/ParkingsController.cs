using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.Parkings.CreateParking;
using SmartParking.Application.Features.Parkings.GetAllParkings;
using SmartParking.Application.Features.Parkings.GetParkingById;
using SmartParking.Application.Features.Parkings.GetParkingQuote;
using SmartParking.Application.Features.Parkings.PublishAvailability;
using SmartParking.Application.Features.Parkings.SearchParkings;
using SmartParking.Contracts.Parkings;

namespace SmartParking.Api.Controllers
{

    [ApiController]
    [Route("api")]
    public sealed class ParkingsController : ControllerBase
    {
        private readonly CreateParkingHandler _createParkingHandler;
        private readonly GetParkingByIdHandler _getParkingByIdHandler;
        private readonly PublishAvailabilityHandler _publishAvailabilityHandler;
        private readonly SearchParkingsHandler _searchParkingsHandler;
        private readonly GetParkingQuoteHandler _getParkingQuoteHandler;
        private readonly GetAllParkingsHandler _getAllParkingsHandler;

        public ParkingsController(
            CreateParkingHandler createParkingHandler,
            GetParkingByIdHandler getParkingByIdHandler,
            PublishAvailabilityHandler publishAvailabilityHandler,
            SearchParkingsHandler searchParkingsHandler,
            GetParkingQuoteHandler getParkingQuoteHandler,
            GetAllParkingsHandler getAllParkingsHandler)
        {
            _createParkingHandler = createParkingHandler;
            _getParkingByIdHandler = getParkingByIdHandler;
            _publishAvailabilityHandler = publishAvailabilityHandler;
            _searchParkingsHandler = searchParkingsHandler;
            _getAllParkingsHandler = getAllParkingsHandler;
            _getParkingQuoteHandler = getParkingQuoteHandler;
        }

        [HttpPost("operators/{operatorId:guid}/parkings")]
        [ProducesResponseType(typeof(ParkingResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            Guid operatorId,
            [FromBody] ParkingCreateRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _createParkingHandler.HandleAsync(operatorId, request, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { parkingId = result.Id },
                result);
        }

        [HttpGet("parkings/{parkingId:guid}")]
        [ProducesResponseType(typeof(ParkingResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            Guid parkingId,
            CancellationToken cancellationToken)
        {
            var result = await _getParkingByIdHandler.HandleAsync(parkingId, cancellationToken);
            return Ok(result);
        }

        [HttpPut("parkings/{parkingId:guid}/availability")]
        [ProducesResponseType(typeof(ParkingAvailabilityResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> PublishAvailability(
            Guid parkingId,
            [FromBody] ParkingAvailabilityUpdateRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _publishAvailabilityHandler.HandleAsync(parkingId, request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("parkings/search")]
        [ProducesResponseType(typeof(IReadOnlyList<ParkingSearchItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search(
    [FromQuery] decimal latitude,
    [FromQuery] decimal longitude,
    [FromQuery] int radiusMeters,
    [FromQuery] DateTime? startTime,
    [FromQuery] DateTime? endTime,
    [FromQuery] bool openOnly = false,
    [FromQuery] string? sortBy = null,
    CancellationToken cancellationToken = default)
        {
            var request = new ParkingSearchRequest(
                latitude,
                longitude,
                radiusMeters,
                startTime,
                endTime,
                openOnly,
                sortBy);

            var result = await _searchParkingsHandler.HandleAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("parkings/{parkingId:guid}/quote")]
        [ProducesResponseType(typeof(ParkingQuoteResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Quote(
            Guid parkingId,
            [FromQuery] DateTime startTime,
            [FromQuery] DateTime endTime,
            CancellationToken cancellationToken)
        {
            var result = await _getParkingQuoteHandler.HandleAsync(
                parkingId,
                startTime,
                endTime,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("parkings")]
        [ProducesResponseType(typeof(IReadOnlyList<ParkingResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _getAllParkingsHandler.HandleAsync(cancellationToken);
            return Ok(result);
        }
    }


}
