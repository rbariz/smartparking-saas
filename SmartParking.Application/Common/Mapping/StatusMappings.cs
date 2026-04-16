using SmartParking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Application.Common.Mapping
{
    public static class StatusMappings
    {
        
        public static string ToApiCode(this BookingStatus status) => status switch
        {
            BookingStatus.PendingPayment => "pending_payment",
            BookingStatus.Confirmed => "confirmed",
            BookingStatus.Expired => "expired",
            BookingStatus.Cancelled => "cancelled",
            BookingStatus.Active => "active",
            BookingStatus.Completed => "completed",
            _ => "unknown"
        };

        public static string ToApiCode(this PaymentStatus status) => status switch
        {
            PaymentStatus.Pending => "pending",
            PaymentStatus.Paid => "paid",
            PaymentStatus.Failed => "failed",
            _ => "unknown"
        };

        public static string ToApiCode(this ParkingStatus status) => status switch
        {
            ParkingStatus.Open => "open",
            ParkingStatus.Closed => "closed",
            _ => "unknown"
        };
        
    }
}
