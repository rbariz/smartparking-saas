using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Contracts.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Operators.GetOperatorById
{

    public sealed class GetOperatorByIdHandler
    {
        private readonly IOperatorRepository _operatorRepository;

        public GetOperatorByIdHandler(IOperatorRepository operatorRepository)
        {
            _operatorRepository = operatorRepository;
        }

        public async Task<OperatorResponse> HandleAsync(
            Guid operatorId,
            CancellationToken cancellationToken = default)
        {
            var entity = await _operatorRepository.GetByIdAsync(operatorId, cancellationToken);
            if (entity is null)
                throw new NotFoundException("Operator not found.");

            return new OperatorResponse(
                entity.Id,
                entity.Name,
                entity.ContactEmail,
                entity.ContactPhone,
                entity.CreatedAtUtc);
        }
    }
}
