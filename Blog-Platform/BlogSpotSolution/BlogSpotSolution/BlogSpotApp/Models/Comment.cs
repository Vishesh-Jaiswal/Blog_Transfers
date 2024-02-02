using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogSpotApp.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? ReportedBy { get; set; } = string.Empty;
        public DateTime? ReportedAt { get; set; }
        public string? ReportReason { get; set; } = string.Empty;
        public DateTime? CommentedAt { get; set; }
        public List<Comment>? ReportedComments { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        [JsonIgnore]
        public User? Commenter { get; set; }
        public int BlogId { get; set; }
        [JsonIgnore]
        public Blog? BlogComment { get; set; }
        public ICollection<CommentLike>? CommentLikes { get; set; }
    }
}