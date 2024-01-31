using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogSpotApp.Models
{
    public class BlogLike
    {
        [Key]
        public int BlogLikeId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public int? BlogId { get; set; }
        [ForeignKey("UserEmail")]
        [JsonIgnore]
        public User? UserLikedBlog { get; set; }
        [ForeignKey("BlogId")]
        [JsonIgnore]
        public Blog? LikedBlog { get; set; }
    }
}
