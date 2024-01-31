using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogSpotApp.Models
{
    public class User
    {


        [Key]
        public string UserEmail { get; set; }=string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public string Role { get; set; } = string.Empty;
        public byte[]? Key { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
        [JsonIgnore]
        public ICollection<Blog>? Blogs { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? UserComments { get; set; }
        [JsonIgnore]
        public ICollection<BlogLike>? LikedBlogs { get; set; }
        [JsonIgnore]
        public ICollection<CommentLike>? LikedComments { get; set; }
        public List<UserFollower>? Followers { get; set; }
        public List<UserFollower>? Followings { get; set; }
    }
}