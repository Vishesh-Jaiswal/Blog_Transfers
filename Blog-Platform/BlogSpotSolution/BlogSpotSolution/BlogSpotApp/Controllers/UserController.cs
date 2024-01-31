using BlogSpotApp.Interfaces;
using BlogSpotApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSpotApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserViewModel viewModel)
        {
           
                try
                {
                    var user = _userService.Register(viewModel);
                    if (user != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (DbUpdateException)
                {
                    ViewBag.Message = "User Email already exits";
                }
                catch (Exception)
                {
                    ViewBag.Message = "Invalid data. Could not register";
                    throw;
                }
            
            return View();
        }

  

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserDTO userDTO)
        {
            var result = _userService.Login(userDTO);
            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "Invalid User Email or password";
            return View();
        }
    }
}
