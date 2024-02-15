using FlightApp.Models;
using FlightApp.Models.DTOs;

namespace FlightApp.Interfaces
{
    public interface IUserService
    {
        public UserDTO? Login(UserDTO user);
        public UserDTO? Register(UserDTO user);
    }
}
