using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogSpotApp.Exceptions;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using Microsoft.AspNetCore.Cors;
using BlogSpotApp.Services;

namespace BlogSpotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogService blogService, ILogger<BlogController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }
        //------------------------------------------CREATE BLOG----------------------------------
        /// <summary>
        /// API to create a blog.
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [Authorize(Roles = "Blogger")]
        [HttpPost]
        [Route("Create")]
        public ActionResult CreateBlog(Blog blog)
        {
            string errorMessage;
            try
            {
                var result = _blogService.AddPost(blog);
                _logger.LogInformation("Blog Created");
                return Ok(result);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }
        //------------------------------------------DELETE BLOG----------------------------------
        /// <summary>
        ///  API to delete a blog.
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [Authorize(Roles = "Blogger,Admin")]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteBlog(Blog blog)
        {
            var result = _blogService.DeletePost(blog);
            if (result != null)
            {
                _logger.LogInformation("Blog Deleted");
                return Ok(result);
            }

            return BadRequest("Blog could not be delete");
        }
        //------------------------------------------EDIT BLOG----------------------------------
        [Authorize(Roles = "Blogger")]
        [HttpPost]
        [Route("Edit")]
        public ActionResult EditBlog(Blog blog)
        {
            var result = _blogService.EditPost(blog);
            if (result != null)
            {
                _logger.LogInformation($"Blog with BlogId {blog.BlogId} was edited");
                return Ok(result);
            }

            return BadRequest("Could Not Edit The Blog");
        }

        [HttpPut]
        [Route("ReportBlog")]
        public ActionResult ReportBlog(Blog blog)
        {
            string errorMessage;
            try
            {
                var result = _blogService.ReportBlog(blog);
                _logger.LogInformation("Comment Reported");
                return Ok(result);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }
        [HttpPut]
        [Route("ApproveReportBlog/{BlogID}")]
        public ActionResult ApproveReportBlog(int BlogID)
        {
            string errorMessage;
            try
            {
                var result = _blogService.ApproveReportBlog(BlogID);
                _logger.LogInformation("Comment Reported");
                return Ok(result);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpGet]
        [Route("ReportedBlogs")]
        public ActionResult GetReportedBlogs()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _blogService.ReportedBlogs();
                _logger.LogInformation("Fetched all the reported blogs");
                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }
        //------------------------------------------GET ALL BLOGS----------------------------------
        [HttpGet]
        public ActionResult Get()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _blogService.GetBlogs();
                _logger.LogInformation("Fetched all the blogs");
                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetBlogById(int id)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _blogService.GetBlogById(id);
                if (result != null)
                {
                    _logger.LogInformation($"Blog with BlogID {id} was fetched");
                    return Ok(result);
                }
                return NotFound();
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpGet]
        [Route("userBlogs/{email}")]
        public ActionResult GetBlogByEmail(string email)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _blogService.GetBlogByEmail(email);
                if (result != null)
                {
                    _logger.LogInformation($"Blogs with User Email {email} was fetched");
                    return Ok(result);
                }
                return NotFound();
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

    }
}
