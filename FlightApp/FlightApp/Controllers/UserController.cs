using FlightApp.Interfaces;
using FlightApp.Models;
using FlightApp.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace FlightApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(UserDTO user)
        {
            string message="";
            try
            {
                var result = _userService.Register(user);
                if (result != null)
                {
                    return Ok(result);
                }
            }catch(DbException)
            {
                message="User Email Already Exists";
            }
            catch (Exception)
            {

            }
            
            return BadRequest(message);
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserDTO user)
        {
            var result = _userService.Login(user);
            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized("UserName Password Mismatch");
        }
    }
}
