using BlogSpotApp.Exceptions;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using BlogSpotApp.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSpotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class BloggerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<BlogController> _logger;

        public BloggerController(IUserService userService, ILogger<BlogController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        //------------------------------------------REGISTER USER----------------------------------
        /// <summary>
        /// API for User Resgistration
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
                    _logger.LogInformation($"{viewModel.UserEmail} has been registered");
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

        //------------------------------------------LOGIN USER----------------------------------
        /// <summary>
        /// API for User Login
        /// </summary>
        /// <param name="userDTO">UserDTO</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserDTO userDTO)
        {
            var user = _userService.Login(userDTO);
            if (user != null )
            {
                _logger.LogInformation($"{userDTO.UserEmail} has been logged in");
                return Ok(user);
            }
            else
            {
                return Unauthorized("Username and Password Mismatch");
            }
        }

        //------------------------------------------GET BLOGGERS----------------------------------
        [HttpGet]
        [Route("GetBloggers")]
        public ActionResult GetBloggers()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _userService.GetBloggers();
                _logger.LogInformation("Fetched all the Bloggers");
                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);

        }

        //------------------------------------------GET READERS----------------------------------
        [HttpGet]
        [Route("GetReaders")]
        public ActionResult GetReaders()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _userService.GetReaders();
                _logger.LogInformation("Fetched all the Readers");
                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        //------------------------------------------GET ALL USERS----------------------------------
        [HttpGet]
        [Route("GetAllUsers")]
        public ActionResult GetAllUsers()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _userService.GetAllUsers();
                _logger.LogInformation("Fetched all the Users");
                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteBlog(UserDTO userDTO)
        {
            var result = _userService.DeleteUser(userDTO);
            if (result != null)
            {
                _logger.LogInformation($"{userDTO.UserEmail} has been deleted");
                return Ok(result);
            }

            return BadRequest("Blog could not be delete");
        }
        [HttpGet]
        [Route("user/{userEmail}")]
        public ActionResult GetUserByEmail(string userEmail)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _userService.GetUserByEmail(userEmail);
                if (result != null)
                {
                    _logger.LogInformation($"Fetched Details of {userEmail}");
                    return Ok(result);
                }
                return NotFound();
            }
            catch (NoSuchUserExists e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpPost]
        [Route("Edit")]
        public ActionResult EditUser([FromForm] ProfilePic profilePic)
        {
            var result = _userService.EditUser(profilePic);
            if (result != null)
            {
                _logger.LogInformation($"Edited User {profilePic.UserEmail}");
                return Ok(result);
            }
            else
            {
                return Unauthorized("Username and Password Mismatch");
            }
        }
    }
}