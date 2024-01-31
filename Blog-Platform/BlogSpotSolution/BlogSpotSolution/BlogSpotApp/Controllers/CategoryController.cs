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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetCategoriesByBlogId(int id)
        {
            string errorMessage = string.Empty;
            try
            {
                var result = _categoryService.GetCategoriesByBlogId(id);
                if (result != null)
                {
                    _logger.LogInformation($"Categories added for blog {id}");
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