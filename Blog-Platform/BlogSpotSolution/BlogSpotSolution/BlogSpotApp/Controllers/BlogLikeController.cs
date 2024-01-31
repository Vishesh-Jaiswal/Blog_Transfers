using Microsoft.AspNetCore.Mvc;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using Microsoft.AspNetCore.Cors;

namespace BlogSpotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class BlogLikeController : ControllerBase
    {
        private readonly IBlogLikeService _blogLikeService;
        private readonly ILogger<BlogLikeController> _logger;
        public BlogLikeController(IBlogLikeService blogLikeService, ILogger<BlogLikeController> logger)
        {
            _blogLikeService = blogLikeService;
            _logger = logger;
        }

        [HttpPost]
        [Route("BlogLikeToggle")]
        public ActionResult BlogLikeToggle(BlogLike blogLike)
        {
            string errorMessage;
            try
            {
                var result = _blogLikeService.BlogLikeToggle(blogLike);
                _logger.LogInformation($"{blogLike.UserEmail} toggled {blogLike.BlogId}");
                return Ok(result);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpPost]
        [Route("BlogLikeStatus")]
        public ActionResult BlogLikeStatus(BlogLike blogLike)
        {
            string errorMessage;
            var result = _blogLikeService.BlogLikeStatus(blogLike);
            if (result != null)
            {
                _logger.LogInformation($"{blogLike.UserEmail} has already liked Blog {blogLike.BlogId}");
                return Ok(true);
            }
            else if (result == null)
            {
                _logger.LogInformation($"{blogLike.UserEmail} has not liked blog {blogLike.BlogId}");
                return Ok(false);
            }
                
            errorMessage = "An error occurred while processing the Blog Like request.";
            return BadRequest(errorMessage);
        }
    }
}