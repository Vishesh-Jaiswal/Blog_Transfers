using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Contexts;
using BlogSpotApp.Models;

namespace BlogSpotApp.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly BlogSpotContext _context;
        public UserRepository(BlogSpotContext context)
        {
            _context = context;
        }
        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? Delete(string key)
        {
            var user = GetById(key);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public IList<User>? GetAll()
        {
            if (_context.Users.Count() == 0)
                return null;
            return _context.Users.ToList();
        }

        public User? GetById(string userEmail)
        {
            var user = _context.Users.Include(u => u.Followers).SingleOrDefault(u => u.UserEmail == userEmail);
            return user;
        }

        public User? Update(User user)
        {
            var editUser = GetById(user.UserEmail);
            if (editUser != null)
            {
                _context.Entry(editUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return editUser;
            }
            return null;
        }
    }
}