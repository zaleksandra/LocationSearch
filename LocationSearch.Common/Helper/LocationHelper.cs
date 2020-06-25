using Domain.Models;
using LocationSearch.Common.Models;
using System;

namespace LocationSearch.Common.Helper
{
    public static class LocationHelper
    {

        public static double ConvertDistanceToDegres(double distance) {
            return distance / 111;
        }
        public static double CalculateDistance(Location location1, Location location2)
        {
            var rlat1 = Math.PI * location2.Latitude / 180;
            var rlat2 = Math.PI * location1.Latitude / 180;
            //var rlon1 = Math.PI * location2.Longitude / 180;
            //var rlon2 = Math.PI * location1.Longitude / 180;
            var theta = location2.Longitude - location1.Longitude;
            var rtheta = Math.PI * theta / 180;
            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1609.344;
        }
        public static bool InRadius(double latitude, double longitude, double maxDistance, Location x)
        {
            return (Math.Pow(x.Latitude - latitude, 2) + Math.Pow(x.Longitude - longitude, 2)) <= Math.Pow(LocationHelper.ConvertDistanceToDegres(maxDistance), 2);
        }
    }
}
