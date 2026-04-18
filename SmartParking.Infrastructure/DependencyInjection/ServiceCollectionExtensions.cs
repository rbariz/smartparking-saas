using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Application.Abstractions.Services;
using SmartParking.Application.Abstractions.Time;
using SmartParking.Application.Features.Bookings.Admin.CancelBooking;
using SmartParking.Application.Features.Bookings.Admin.CompleteBooking;
using SmartParking.Application.Features.Bookings.Admin.ExpireBooking;
using SmartParking.Application.Features.Bookings.CreateBooking;
using SmartParking.Application.Features.Bookings.GetAllBookings;
using SmartParking.Application.Features.Bookings.GetBooking;
using SmartParking.Application.Features.Bookings.ListDriverBookings;
using SmartParking.Application.Features.DriverAuth.RequestDriverOtp;
using SmartParking.Application.Features.DriverAuth.VerifyDriverOtp;
using SmartParking.Application.Features.Drivers.CreateDriver;
using SmartParking.Application.Features.Drivers.GetDriverById;
using SmartParking.Application.Features.Operators.CreateOperator;
using SmartParking.Application.Features.Operators.GetOperatorById;
using SmartParking.Application.Features.Parkings.CreateParking;
using SmartParking.Application.Features.Parkings.GetAllParkings;
using SmartParking.Application.Features.Parkings.GetAllPayments;
using SmartParking.Application.Features.Parkings.GetParkingById;
using SmartParking.Application.Features.Parkings.GetParkingQuote;
using SmartParking.Application.Features.Parkings.PublishAvailability;
using SmartParking.Application.Features.Parkings.SearchParkings;
using SmartParking.Application.Features.Payments.ConfirmPayment;
using SmartParking.Application.Features.Payments.CreatePayment;
using SmartParking.Application.Features.Payments.GetPayment;
using SmartParking.Infrastructure.Persistence;
using SmartParking.Infrastructure.Persistence.Repositories;
using SmartParking.Infrastructure.Services;
using SmartParking.Infrastructure.Time;

namespace SmartParking.Infrastructure.DependencyInjection
{
    
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SmartParkingDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOperatorRepository, OperatorRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IParkingRepository, ParkingRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<IClock, SystemClock>();
            services.AddScoped<IBookingPricingService, BookingPricingService>();

            services.AddScoped<IBookingExpirationService, BookingExpirationService>();

            services.AddScoped<IDriverOtpChallengeRepository, DriverOtpChallengeRepository>();
            services.AddScoped<IDriverTokenService, DriverTokenService>();



            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateOperatorHandler>();
            services.AddScoped<GetOperatorByIdHandler>();


            services.AddScoped<CreateDriverHandler>();
            services.AddScoped<GetDriverByIdHandler>();

            services.AddScoped<CreateParkingHandler>();
            services.AddScoped<PublishAvailabilityHandler>();
            services.AddScoped<SearchParkingsHandler>();
            services.AddScoped<GetParkingQuoteHandler>();
            services.AddScoped<GetParkingByIdHandler>();
            services.AddScoped<GetAllParkingsHandler>();

            services.AddScoped<CreateBookingHandler>();
            services.AddScoped<GetBookingHandler>();
            services.AddScoped<ListDriverBookingsHandler>();
            services.AddScoped<IBookingLifecycleService, BookingLifecycleService>();
            services.AddScoped<GetAllBookingsHandler>();
            services.AddScoped<CancelBookingHandler>();
            services.AddScoped<CompleteBookingHandler>();
            services.AddScoped<ExpireBookingHandler>();

            services.AddScoped<CreatePaymentHandler>();
            services.AddScoped<ConfirmPaymentHandler>();
            services.AddScoped<GetPaymentHandler>();
            services.AddScoped<GetAllPaymentsHandler>();

            services.AddScoped<RequestDriverOtpHandler>();
            services.AddScoped<VerifyDriverOtpHandler>();


            return services;
        }
    }
}
