namespace SmartParking.Mobile.Driver.Services
{
    public static class DistanceHelper
    {
        public static double CalculateDistanceKm(
            decimal fromLatitude,
            decimal fromLongitude,
            decimal toLatitude,
            decimal toLongitude)
        {
            const double earthRadiusKm = 6371.0;

            var dLat = DegreesToRadians((double)(toLatitude - fromLatitude));
            var dLon = DegreesToRadians((double)(toLongitude - fromLongitude));

            var lat1 = DegreesToRadians((double)fromLatitude);
            var lat2 = DegreesToRadians((double)toLatitude);

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2) *
                Math.Cos(lat1) * Math.Cos(lat2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        public static string FormatDistanceKm(double distanceKm)
        {
            if (distanceKm < 1)
            {
                var meters = distanceKm * 1000;
                return $"{meters:0} m";
            }

            return $"{distanceKm:0.0} km";
        }

        private static double DegreesToRadians(double degrees)
            => degrees * Math.PI / 180.0;
    }

}
