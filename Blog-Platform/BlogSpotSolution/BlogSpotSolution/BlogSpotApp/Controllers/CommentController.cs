using BlogSpotApp.Exceptions;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using BlogSpotApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BlogSpotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        [Authorize(Roles = "Reader")]
        [HttpPost]
        [Route("AddComment")]
        public ActionResult AddComment(Comment comment)
        {
            string errorMessage;
            try
            {
                var result = _commentService.AddComment(comment);
                _logger.LogInformation("Comment Added");
                return Ok(result);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }

        [HttpPut]
        [Route("ReportComment")]
        public ActionResult ReportComment(Comment comment)
        {
            string errorMessage;
            try
            {
                var result = _commentService.ReportComment(comment);
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
        [Route("ApproveReportComment/{commentID}")]
        public ActionResult ApproveReportComment(int commentID)
        {
            string errorMessage;
            try
            {
                var result = _commentService.ApproveReportComment(commentID);
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
        [Route("ReportedComments")]
        public ActionResult GetReportedComments()
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _commentService.ReportedComments();
                _logger.LogInformation("Fetched all the reported comments");
                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }


        [HttpPost]
        [Route("EditComment")]
        public ActionResult EditComment(Comment comment)
        {
            var result = _commentService.EditComment(comment);
            if (result != null)
            {
                _logger.LogInformation($"Comment with CommentID {comment.CommentId} was edited");
                return Ok(result);
            }

            return BadRequest("Could Not Edit Comment");
        }

        [HttpGet]
        [Route("{id}")]
        
        public ActionResult Get(int id)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _commentService.GetCommentsById(id);
                _logger.LogInformation($"Fetched Comment with commentID {id}");
                return Ok(result);
            }
            catch (NoBlogsAvailableException e)
            {
                errorMessage = e.Message;
            }
            return BadRequest(errorMessage);
        }
        [HttpGet]
        [Route("userComments/{userEmail}")]
        public ActionResult GetCommentsByEmail(string userEmail)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _commentService.GetCommentsByEmail(userEmail);
                _logger.LogInformation($"Fetched Comment for user: {userEmail}");
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
        public ActionResult DeleteComment(Comment comment)
        {
            var result = _commentService.DeleteComment(comment);
            if (result != null)
            {
                _logger.LogInformation($"Deleted Comment with commentID {comment.CommentId}");
                return Ok(result);
            }

            return BadRequest("Blog could not be delete");
        }
    }
}
