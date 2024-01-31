using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogSpotApp.Models
{
    public class UserFollower
    {
        [Key]
        public int RelationId { get; set; }
        public string FollowerId { get; set; } = string.Empty;
        [JsonIgnore]
        public User? Follower { get; set; }
        public string FollowingId { get; set; }=string.Empty;
        [JsonIgnore]
        public User? Following { get; set; }
    }
}
