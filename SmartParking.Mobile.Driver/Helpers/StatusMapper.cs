using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Mobile.Driver.Helpers
{


    public static class StatusMapper
    {
        public static string MapParkingStatus(string? status, IStringLocalizer<AppResources> L)
        {
            if (string.IsNullOrWhiteSpace(status))
                return string.Empty;

            return status.Trim().ToLowerInvariant() switch
            {
                "open" => L["Status_Open"],
                "closed" => L["Status_Closed"],
                "full" => L["Status_Full"],
                _ => status
            };
        }

        public static string MapBookingStatus(string? status, IStringLocalizer<AppResources> L)
        {
            if (string.IsNullOrWhiteSpace(status))
                return string.Empty;

            return status.Trim().ToLowerInvariant() switch
            {
                "pending_payment" => L["BookingStatus_PendingPayment"],
                "confirmed" => L["BookingStatus_Confirmed"],
                "active" => L["BookingStatus_Active"],
                "completed" => L["BookingStatus_Completed"],
                "cancelled" => L["BookingStatus_Cancelled"],
                "expired" => L["BookingStatus_Expired"],
                _ => status
            };
        }

        public static string MapPaymentStatus(string? status, IStringLocalizer<AppResources> L)
        {
            if (string.IsNullOrWhiteSpace(status))
                return string.Empty;

            return status.Trim().ToLowerInvariant() switch
            {
                "pending" => L["PaymentStatus_Pending"],
                "paid" => L["PaymentStatus_Paid"],
                "failed" => L["PaymentStatus_Failed"],
                "cancelled" => L["PaymentStatus_Cancelled"],
                _ => status
            };
        }
    }
}
