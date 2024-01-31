using BlogSpotApp.Models.DTOs;

namespace BlogSpotApp.Interfaces
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}