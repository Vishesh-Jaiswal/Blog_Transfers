using OnlineBookStore.Models;

namespace OnlineBookStore.Interfaces
{
    public interface IBookService
    {
        Book AddBook(Book book);
        Book GetBookById (int id);
        Book UpdateBook(Book book);
        Book DeleteBook(int id);
        IList<Book> GetAllBooks();
    }
}
