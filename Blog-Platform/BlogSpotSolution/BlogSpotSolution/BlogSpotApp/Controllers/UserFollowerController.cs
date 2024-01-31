using BlogSpotApp.Exceptions;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using BlogSpotApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSpotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class UserFollowerController : ControllerBase
    {
        private readonly IUserFollowerService _followService;

        public UserFollowerController(IUserFollowerService followService)
        {
            _followService = followService;
        }

        [HttpPost]
        [Route("Follow")]
        public ActionResult Follow(UserFollower userFollower)
        {
            var user = _followService.ToggleFollower(userFollower);
            if (user!=null)
            {
                return Ok(user);
            }
            else
            {
                return Unauthorized("Already following");
            }
        }
        [HttpGet]
        [Route("followers/{userEmail}")]
        public ActionResult GetFollowers(string userEmail)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _followService.GetFollowers(userEmail);

                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpGet]
        [Route("followees/{userEmail}")]
        public ActionResult GetFollowees(string userEmail)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _followService.GetFollowees(userEmail);

                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpPost]
        [Route("Status")]
        public ActionResult FollowStatus(UserFollower userFollower)
        {
            string errorMessage = string.Empty;
            var result = _followService.FollowStatus(userFollower);
            if(result!=null)
                return Ok(true);
            else if(result==null)
                return Ok(false);
            return BadRequest(errorMessage);
        }
    }
}
