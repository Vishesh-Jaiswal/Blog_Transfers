using FlightApp.Contexts;
using FlightApp.Interfaces;
using FlightApp.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightApp.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly FlightAppDBContext _dbContext;

        public UserRepository(FlightAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Add(User key)
        {
            _dbContext.Users.Add(key);
            _dbContext.SaveChanges();
            return key;
        }

        public User Delete(string key)
        {
            var result=GetById(key);
            if (result != null)
            {
                _dbContext.Users.Remove(result);
                _dbContext.SaveChanges();
                return result;
            }
            return null;
        }

        public IList<User> GetAll()
        {
            if (_dbContext.Users.Count() == 0)
                return null;
            return _dbContext.Users.ToList();
        }

        public User GetById(string key)
        {
            var result = _dbContext.Users.SingleOrDefault(u => u.UserEmail == key);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public User Update(User key)
        {
            var result = GetById(key.UserEmail);
            if (result != null)
            {
                _dbContext.Users.Update(result);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}
