using FlightApp.Interfaces;
using FlightApp.Models;

namespace FlightApp.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository<string, User> _userRepository;

        public UserService(IRepository<string, User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(User user)
        {
            var result = _userRepository.GetById(user.UserEmail);

            if((result != null) && (result.UserEmail==user.UserEmail) && (result.UserPassword==user.UserPassword))
            {
                return result;
            }
            return null;
        }

        public User Register(User user)
        {
            var result=_userRepository.Add(user);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
