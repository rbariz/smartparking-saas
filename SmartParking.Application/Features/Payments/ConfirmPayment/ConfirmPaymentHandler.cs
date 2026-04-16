using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Payments;
using SmartParking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Payments.ConfirmPayment
{


    public sealed class ConfirmPaymentHandler
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmPaymentHandler(
            IPaymentRepository paymentRepository,
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentResponse> HandleAsync(
            Guid paymentId,
            PaymentConfirmRequest request,
            CancellationToken cancellationToken = default)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId, cancellationToken);
            if (payment is null)
                throw new NotFoundException("Payment not found.");

            var booking = await _bookingRepository.GetByIdAsync(payment.BookingId, cancellationToken);
            if (booking is null)
                throw new NotFoundException("Booking not found.");

            if (booking.Status != BookingStatus.PendingPayment)
                throw new ConflictException("Booking is not pending payment.");

            payment.MarkPaid(request.ProviderReference);
            booking.Confirm();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PaymentResponse(
                payment.Id,
                payment.BookingId,
                payment.Amount,
                payment.Currency,
                payment.Status.ToApiCode(),
                payment.Method,
                payment.ProviderReference,
                payment.CreatedAtUtc,
                payment.PaidAtUtc);
        }
    }
}
