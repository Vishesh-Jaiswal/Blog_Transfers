using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Interfaces;
using OnlineBookStore.Models.DTOs;

namespace OnlineBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(UserDTO viewModel)
        {

            string message = "";
            try
            {
                var user = _userService.Register(viewModel);
                if (user != null)
                {
                    return Ok(user);
                }
            }
            catch (DbUpdateException)
            {
                message = "This Email already exists";
            }
            catch (Exception)
            {

            }
            return BadRequest(message);
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserDTO userDTO)
        {
            var user = _userService.Login(userDTO);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return Unauthorized("Username and Password Mismatch");
            }
        }
    }
}
