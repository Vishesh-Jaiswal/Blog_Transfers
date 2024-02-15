using FlightApp.Models.DTOs;

namespace FlightApp.Interfaces
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}