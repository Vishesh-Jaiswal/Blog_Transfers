using Microsoft.AspNetCore.Mvc;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using Microsoft.AspNetCore.Cors;
using BlogSpotApp.Exceptions;
using BlogSpotApp.Services;

namespace BlogSpotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class CommentLikeController : ControllerBase
    {
        private readonly ICommentLikeService _commentLikeService;
        private readonly ILogger<CommentLikeController> _logger;

        public CommentLikeController(ICommentLikeService commentLikeService, ILogger<CommentLikeController> logger)
        {
            _commentLikeService = commentLikeService;
            _logger = logger;
        }

        [HttpPost]
        [Route("CommentLikeStatus")]
        public ActionResult CommentLikeStatus(CommentLike commentLike)
        {
            string errorMessage = string.Empty; ;
            var result = _commentLikeService.CommentLikeStatus(commentLike);
            if (result != null)
            {
                _logger.LogInformation($"{commentLike.UserEmail} has already liked comment {commentLike.CommentId}");
                return Ok(true);
            }
            else if (result == null)
            {
                _logger.LogInformation($"{commentLike.UserEmail} has not liked comment {commentLike.CommentId}");
                return Ok(false);
            }
            errorMessage = "An error occurred while processing the CommentLike request.";
            return BadRequest(errorMessage);
        }

        [HttpPost]
        [Route("CommentLikeToggle")]
        public ActionResult CommentLikeToggle(CommentLike commentLike)
        {
            string errorMessage;
            try
            {
                var result = _commentLikeService.CommentLikeToggle(commentLike);
                _logger.LogInformation($"{commentLike.UserEmail} toggled {commentLike.CommentId}");
                return Ok(result);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpGet]
        [Route("{blogId:int}/{userEmail}")]
        public ActionResult Get(int blogId, string userEmail)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _commentLikeService.CommentLikesByBlog(blogId, userEmail);
                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

    }
}