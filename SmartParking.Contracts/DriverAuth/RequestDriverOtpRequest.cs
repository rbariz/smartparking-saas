
namespace SmartParking.Contracts.DriverAuth
{
    public sealed record RequestDriverOtpRequest(
     string Contact,
     string Channel // "email" or "sms"
 );
}
