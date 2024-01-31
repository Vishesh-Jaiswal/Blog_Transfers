using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogSpotApp.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime? CommentedAt { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        [JsonIgnore]
        public User? Commenter { get; set; }

        public int BlogId { get; set; }
        [JsonIgnore]
        public Blog? BlogComment { get; set; }
        public ICollection<CommentLike>? CommentLikes { get; set; }
    }
}