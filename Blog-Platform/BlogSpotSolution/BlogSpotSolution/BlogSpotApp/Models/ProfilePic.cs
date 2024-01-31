namespace BlogSpotApp.Models
{
    public class ProfilePic
    {
        public string? UserEmail { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateofBirth { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}
