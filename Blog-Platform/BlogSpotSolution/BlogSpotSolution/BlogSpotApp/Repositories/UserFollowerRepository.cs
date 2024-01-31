using BlogSpotApp.Contexts;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using System.Reflection.Metadata;

namespace BlogSpotApp.Repositories
{
    public class UserFollowerRepository : IRepository<int, UserFollower>
    {
        private readonly BlogSpotContext _context;

        public UserFollowerRepository(BlogSpotContext context)
        {
            _context = context;
        }

        public UserFollower Add(UserFollower UserFollower)
        {
            _context.UserFollowers.Add(UserFollower);
            _context.SaveChanges();
            return UserFollower;
        }

        public UserFollower? Delete(int followRelationId)
        {
            var followRelation = GetById(followRelationId);
            if (followRelation != null)
            {
                _context.UserFollowers.Remove(followRelation);
                _context.SaveChanges();
                return followRelation;
            }
            return null;
        }

        public IList<UserFollower>? GetAll()
        {
            if (_context.UserFollowers.Count() == 0)
                return null;
            return _context.UserFollowers.ToList();
        }

        public UserFollower? GetById(int followRelationId)
        {
            var followRelation = _context.UserFollowers.SingleOrDefault(b => b.RelationId == followRelationId);
            return followRelation;
        }

        public UserFollower? Update(UserFollower entity)
        {
            throw new NotImplementedException();
        }
    }
}
