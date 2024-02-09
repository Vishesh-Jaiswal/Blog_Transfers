using OnlineBookStore.Interfaces;
using OnlineBookStore.Models;
using OnlineBookStore.Exceptions;

namespace OnlineBookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<int, Book> _bookRepository;

        public BookService(IRepository<int, Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book? AddBook(Book book)
        {
            var result=_bookRepository.Add(book);
            if(result != null)
            {
                return result;
            }
            return null;
        }

        public Book? DeleteBook(int id)
        {
            var result = _bookRepository.Delete(id);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IList<Book>? GetAllBooks()
        {
            var result = _bookRepository?.GetAll();
            if (result?.Count == 0 || result==null)
            {
                return null;
            }
            return result;
        }

        public IList<Book>? GetBooksByAuthor(string author)
        {
            var result = _bookRepository.GetAll()?.Where(x => x.Author == author).ToList();
            if (result?.Count == 0 || result == null)
            {
                return null;
            }
            return result;
        }
        public IList<Book>? GetBooksByGenre(string genre)
        {
            var result = _bookRepository.GetAll()?.Where(x => x.Genre == genre).ToList();
            if (result?.Count == 0 || result == null)
            {
                return null;
            }
            return result;
        }

        public Book? GetBookById(int id)
        {
            var result = _bookRepository.GetById(id);
            if (result == null)
            {
                throw new BookNotFound();
            }
            return result;
        }

        public Book? UpdateBook(Book book)
        {
            var result = _bookRepository.Update(book);
            if (result == null)
            {
                return null;
            }
            return result;
        }
    }
}
