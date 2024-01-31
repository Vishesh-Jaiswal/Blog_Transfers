using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogSpotApp.Models
{
    public class CommentLike
    {
        [Key]
        public int CommentLikeId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public int BlogId { get; set; }
        public int? CommentId { get; set; }
        [ForeignKey("UserEmail")]
        [JsonIgnore]
        public User? UserLikedComment { get; set; }
        [ForeignKey("CommentId")]
        [JsonIgnore]
        public Comment? LikedComment { get; set; }

    }
}
