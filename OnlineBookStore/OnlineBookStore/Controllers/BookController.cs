using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Interfaces;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("AddBook")]
        public ActionResult AddBook(Book book)
        {
            string errorMessage="Failed";
            var result=_bookService.AddBook(book);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest(errorMessage);
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteBook")]
        public ActionResult DeleteBook(int id)
        {
            string errorMessage= "Failed";
            var result = _bookService.DeleteBook(id);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest(errorMessage);
        }



        [HttpGet]
        [Route("GetAllBooks")]
        public ActionResult GetBooks()
        {
            string errorMessage = "Failed";
            var result = _bookService.GetAllBooks();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(errorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("UpdateBook")]
        public ActionResult UpdateBook(Book book)
        {
            string errorMessage = "Failed";
            var result = _bookService.UpdateBook(book);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(errorMessage);
        }

        [HttpGet]
        [Route("GetBookById/{id}")]
        public ActionResult GetBookById(int id)
        {
            string errorMessage = "Failed";
            var result = _bookService.GetBookById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(errorMessage);
        }

        [HttpGet]
        [Route("GetBooksByAuthor/{author}")]
        public ActionResult GetBooksByAuthor(string author)
        {
            string errorMessage = "Failed";
            var result = _bookService.GetBooksByAuthor(author);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(errorMessage);
        }
        [HttpGet]
        [Route("GetBooksByGenre/{genre}")]
        public ActionResult GetBooksByGenre(string genre)
        {
            string errorMessage = "Failed";
            var result = _bookService.GetBooksByGenre(genre);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(errorMessage);
        }
    }
}
