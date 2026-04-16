using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.Drivers.CreateDriver;
using SmartParking.Application.Features.Drivers.GetDriverById;
using SmartParking.Contracts.Drivers;

namespace SmartParking.Api.Controllers
{

    [ApiController]
    [Route("api/drivers")]
    public sealed class DriversController : ControllerBase
    {
        private readonly CreateDriverHandler _createDriverHandler;
        private readonly GetDriverByIdHandler _getDriverByIdHandler;

        public DriversController(
            CreateDriverHandler createDriverHandler,
            GetDriverByIdHandler getDriverByIdHandler)
        {
            _createDriverHandler = createDriverHandler;
            _getDriverByIdHandler = getDriverByIdHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DriverResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] DriverCreateRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _createDriverHandler.HandleAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { driverId = result.Id },
                result);
        }

        [HttpGet("{driverId:guid}")]
        [ProducesResponseType(typeof(DriverResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            Guid driverId,
            CancellationToken cancellationToken)
        {
            var result = await _getDriverByIdHandler.HandleAsync(driverId, cancellationToken);
            return Ok(result);
        }
    }
}
