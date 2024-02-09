using OnlineBookStore.Models;

namespace OnlineBookStore.Interfaces
{
    public interface IBookService
    {
        Book? AddBook(Book book);
        Book? GetBookById (int id);
        Book? UpdateBook(Book book);
        Book? DeleteBook(int id);
        IList<Book>? GetAllBooks();
        IList<Book>? GetBooksByGenre(string genre);
        IList<Book>? GetBooksByAuthor(string author);
    }
}
