using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Common;
using SmartParking.Application.Common.Mapping;
using SmartParking.Contracts.Payments;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Features.Payments.CreatePayment
{


    public sealed class CreatePaymentHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaymentHandler(
            IBookingRepository bookingRepository,
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentResponse> HandleAsync(
            Guid bookingId,
            PaymentCreateRequest request,
            CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
            if (booking is null)
                throw new NotFoundException("Booking not found.");

            if (booking.Status != BookingStatus.PendingPayment)
                throw new ConflictException("Payment can only be created for pending payment bookings.");

            var payment = new Payment(
                booking.Id,
                booking.ReservedPrice,
                booking.PriceCurrency,
                request.Method);

            await _paymentRepository.AddAsync(payment, cancellationToken);
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
