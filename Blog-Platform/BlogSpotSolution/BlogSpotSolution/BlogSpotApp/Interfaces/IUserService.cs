using BlogSpotApp.Models;
using BlogSpotApp.Models.DTOs;

namespace BlogSpotApp.Interfaces
{
    public interface IUserService
    {
        UserDTO? Login(UserDTO userDTO);
        UserDTO? Register(UserDTO userDTO);
        UserDTO? DeleteUser(UserDTO userDTO);
        List<User> GetBloggers();
        List<User> GetReaders();
        List<User> GetAllUsers();
        User? GetUserByEmail(string userEmail);
        ProfilePic? EditUser(ProfilePic user);
    }
}