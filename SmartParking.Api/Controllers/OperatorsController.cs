using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.Operators.CreateOperator;
using SmartParking.Application.Features.Operators.GetOperatorById;
using SmartParking.Contracts.Operators;

namespace SmartParking.Api.Controllers
{

    [ApiController]
    [Route("api/operators")]
    public sealed class OperatorsController : ControllerBase
    {
        private readonly CreateOperatorHandler _createOperatorHandler;
        private readonly GetOperatorByIdHandler _getOperatorByIdHandler;

        public OperatorsController(
            CreateOperatorHandler createOperatorHandler,
            GetOperatorByIdHandler getOperatorByIdHandler)
        {
            _createOperatorHandler = createOperatorHandler;
            _getOperatorByIdHandler = getOperatorByIdHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperatorResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] OperatorCreateRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _createOperatorHandler.HandleAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { operatorId = result.Id },
                result);
        }

        [HttpGet("{operatorId:guid}")]
        [ProducesResponseType(typeof(OperatorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            Guid operatorId,
            CancellationToken cancellationToken)
        {
            var result = await _getOperatorByIdHandler.HandleAsync(operatorId, cancellationToken);
            return Ok(result);
        }
    }
}
