using System.ComponentModel.DataAnnotations;

namespace FlightApp.Models
{
    public class User
    {
        [Key]
        [Required]
        public string UserEmail { get; set; }=string.Empty;
        public string UserName { get; set; } = string.Empty;
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public string Role { get; set; } = string.Empty;
        public byte[]? Key { get; set; }
        public ICollection<Flight>? flights { get; set; }
    }
}
