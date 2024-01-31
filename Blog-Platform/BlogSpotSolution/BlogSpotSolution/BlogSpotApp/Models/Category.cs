using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSpotApp.Models
{
    public class Category
    {
        [Key]
        public int RelationId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int BlogId { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
    }
}