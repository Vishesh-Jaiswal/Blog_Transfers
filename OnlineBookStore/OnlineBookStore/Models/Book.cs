using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublichDate { get; set; }
        public User BooksTakenBy { get; set; }
        public string UserEmail { get; set; }
    }
}
