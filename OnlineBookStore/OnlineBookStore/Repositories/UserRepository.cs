using OnlineBookStore.Contexts;
using OnlineBookStore.Interfaces;
using OnlineBookStore.Models;

namespace OnlineBookStore.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly OnlineBookAppContext _onlineBookAppContext;

        public UserRepository(OnlineBookAppContext onlineBookAppContext)
        {
            _onlineBookAppContext = onlineBookAppContext;
        }

        public User Add(User entity)
        {
            _onlineBookAppContext.Users?.Add(entity);
            _onlineBookAppContext.SaveChanges();
            return entity;
            
        }

        public User? Delete(string key)
        {
            throw new NotImplementedException();
        }

        public IList<User>? GetAll()
        {
            throw new NotImplementedException();
        }

        public User? GetById(string key)
        {
            throw new NotImplementedException();
        }

        public User? Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
