namespace SmartParking.Mobile.Driver.Services
{
    public static class UiFormatters
    {
        public static string DateTimeShort(DateTime? value)
            => value?.ToLocalTime().ToString("dd MMM HH:mm") ?? "-";

        public static string Money(decimal amount, string? currency)
            => $"{amount:0.##} {currency}";

        public static string DistanceFromMeters(int meters)
        {
            if (meters < 1000)
                return $"{meters} m";

            var km = meters / 1000.0;
            return $"{km:0.0} km";
        }
    }

}
