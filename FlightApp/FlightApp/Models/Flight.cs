using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace FlightApp.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
        public string Airlines { get; set; }=string.Empty;
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;
        public DateTime Departure {  get; set; }
        public DateTime Arrival { get; set; }
        public double Price { get; set; }
        public User? User { get; set; }
        public string? UserEmail { get; set; }
    }
}
