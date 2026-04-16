namespace SmartParking.Application.Common.Geo
{
    public static class DistanceCalculator
    {
        public static int CalculateDistanceMeters(
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

            var distanceKm = earthRadiusKm * c;
            var distanceMeters = distanceKm * 1000;

            return (int)Math.Round(distanceMeters, MidpointRounding.AwayFromZero);
        }

        private static double DegreesToRadians(double degrees)
            => degrees * Math.PI / 180.0;
    }
}
