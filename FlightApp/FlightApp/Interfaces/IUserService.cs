using FlightApp.Models;

namespace FlightApp.Interfaces
{
    public interface IUserService
    {
        public User Login(User user);
        public User Register(User user);
    }
}
