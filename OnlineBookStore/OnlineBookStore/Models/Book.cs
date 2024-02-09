using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime? PublichDate { get; set; }
        public User? BooksTakenBy { get; set; }
        public string? UserEmail { get; set; }
    }
}
