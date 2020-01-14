using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPITest.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string first_name { get; set; }

        [Required]
        [StringLength(100)]
        public string last_name { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(15)]
        public string ip_address { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public bool IsWithinDistance(double targetLatitude, double targetLongitude, int range)
        {
            /*
                Haversine formula: a = sin²(Δφ/2) + cos φ1 ⋅ cos φ2 ⋅ sin²(Δλ/2)
                c = 2 * atan2( √a, √(1−a) )
                d = RADIUS * c
                where φ is latitude, λ is longitude, RADIUS is earth’s radius (mean radius = 3963 miles);

                https://www.simongilbert.net/parallel-haversine-formula-dotnetcore/
            */

            const double EarthRadiusInMiles = 3963D; // Earth's radius in miles
            const double LondonLatitude = 51.509865;
            const double LondonLongitude = -0.118092;
            const double DegreesToRadians = (Math.PI / 180D);

            var deltaLatitude = (targetLatitude - LondonLatitude) * DegreesToRadians; //convert the radiant to degree
            var deltaLongitude = (targetLongitude - LondonLongitude) * DegreesToRadians;

            var a = Math.Pow(
                Math.Sin(deltaLatitude / 2D), 2D) +
                Math.Cos(LondonLatitude * DegreesToRadians) *
                Math.Cos(targetLatitude * DegreesToRadians) *
                Math.Pow(Math.Sin(deltaLongitude / 2D), 2D);

            var c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));

            var distance = EarthRadiusInMiles * c;

            return (Math.Round(distance) <= range);
        }
    }
}