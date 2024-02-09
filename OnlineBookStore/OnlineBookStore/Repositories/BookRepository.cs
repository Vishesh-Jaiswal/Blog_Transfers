using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using OnlineBookStore.Contexts;
using OnlineBookStore.Interfaces;
using OnlineBookStore.Models;

namespace OnlineBookStore.Repositories
{
    public class BookRepository : IRepository<int, Book>
    {
        private readonly OnlineBookAppContext _onlineBookAppContext;

        public BookRepository(OnlineBookAppContext onlineBookAppContext)
        {
            _onlineBookAppContext = onlineBookAppContext;
        }

        public Book Add(Book entity)
        {
            _onlineBookAppContext.Books?.Add(entity);
            _onlineBookAppContext.SaveChanges();
            return entity;
        }

        public Book? Delete(int key)
        {
            var result=GetById(key);
            if (result != null)
            {
                _onlineBookAppContext.Books?.Remove(result);
                _onlineBookAppContext.SaveChanges();
                return result;
            }
            return null;
        }

        public IList<Book>? GetAll()
        {
            if (_onlineBookAppContext.Books?.Count() != 0)
            {
                return _onlineBookAppContext.Books?.ToList();
            }
            return null;
        }

        public Book? GetById(int key)
        {
            var result=_onlineBookAppContext.Books?.SingleOrDefault(b=>b.BookId==key);
            if(result != null)
            {
                return result;
            }
            return null;
        }

        public Book? Update(Book entity)
        {
            var result = GetById(entity.BookId);
            if(result!=null)
            {
                _onlineBookAppContext.Entry(result).CurrentValues.SetValues(entity);
                _onlineBookAppContext.SaveChanges();
                return result;
            }
            return null;
        }
    }
}
