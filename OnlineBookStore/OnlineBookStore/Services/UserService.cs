using OnlineBookStore.Interfaces;
using OnlineBookStore.Models;
using OnlineBookStore.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBookStore.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<string, User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public UserDTO? Register(UserDTO userDTO)
        {
            HMACSHA512 hmac = new HMACSHA512();
            User user = new User()
            {
                UserEmail = userDTO.UserEmail,
                UserName = userDTO.UserName,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                Key = hmac.Key,

                Role = userDTO.Role,
            };
            var result = _userRepository.Add(user);
            if (result != null)
            {
                userDTO.Password = "";
                return userDTO;
            }
            return null;
        }

        public UserDTO? Login(UserDTO userDTO)
        {
            if (userDTO == null || userDTO.UserEmail == null || userDTO.Password == null)
            {
                return null;
            }
            var user = _userRepository.GetById(userDTO.UserEmail);
            if (user != null && user.Key != null)
            {
                HMACSHA512 hmac = new HMACSHA512(user.Key);
                var userpass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userpass.Length; i++)
                {
                    if (user.Password[i] != userpass[i])
                        return null;
                }
                userDTO.UserName = user.UserName ?? "";
                userDTO.Role = user.Role;
                userDTO.Token = _tokenService.GetToken(userDTO);
                userDTO.Password = "";
                return userDTO;
            }
            return null;
        }
    }
}
