using OnlineBookStore.Models;
using OnlineBookStore.Models.DTOs;
namespace OnlineBookStore.Interfaces
{
    public interface IUserService
    {
        UserDTO? Login(UserDTO user);
        UserDTO? Register(UserDTO user);
    }
}
