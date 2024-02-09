using System.ComponentModel.DataAnnotations;
namespace OnlineBookStore.Models.DTOs
{
    public class UserDTO
    {

        [Required(ErrorMessage = "User Email cannot be empty")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; } = string.Empty;


    }
}