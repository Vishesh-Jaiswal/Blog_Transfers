using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models
{
    public class User
    {
        [Key]
        public string UserEmail { get; set; }=string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public string Role { get; set; } = string.Empty;
        public byte[]? Key { get; set; }
        public ICollection<Book>? BooksTaken { get; set; }

    }
}