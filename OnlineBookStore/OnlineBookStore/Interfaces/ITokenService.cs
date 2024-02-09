using OnlineBookStore.Models.DTOs;

namespace OnlineBookStore.Interfaces
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}