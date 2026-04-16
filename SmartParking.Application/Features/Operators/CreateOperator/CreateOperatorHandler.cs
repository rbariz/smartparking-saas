using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Contracts.Operators;
using SmartParking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Operators.CreateOperator
{


    public sealed class CreateOperatorHandler
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOperatorHandler(
            IOperatorRepository operatorRepository,
            IUnitOfWork unitOfWork)
        {
            _operatorRepository = operatorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperatorResponse> HandleAsync(
            OperatorCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            var entity = new Operator(
                request.Name,
                request.ContactEmail,
                request.ContactPhone);

            await _operatorRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OperatorResponse(
                entity.Id,
                entity.Name,
                entity.ContactEmail,
                entity.ContactPhone,
                entity.CreatedAtUtc);
        }
    }
}
