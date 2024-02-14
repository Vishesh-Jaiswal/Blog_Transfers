using System.ComponentModel.DataAnnotations;

namespace FlightApp.Models
{
    public class User
    {
        [Key]
        [Required]
        public string UserEmail { get; set; }=string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserPassword { get; set; } =string.Empty;
        public ICollection<Flight>? flights { get; set; }
    }
}
