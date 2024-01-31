using BlogSpotApp.Contexts;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using System.Reflection.Metadata;

namespace BlogSpotApp.Repositories
{
    public class BlogLikeRepository : IRepository<int, BlogLike>
    {
        private readonly BlogSpotContext _context;

        public BlogLikeRepository(BlogSpotContext context)
        {
            _context = context;
        }

        public BlogLike Add(BlogLike blogLike)
        {
            _context.BlogLikes.Add(blogLike);
            _context.SaveChanges();
            return blogLike;
        }

        public BlogLike? Delete(int key)
        {
            var blogLike = GetById(key);
            if (blogLike != null)
            {
                _context.BlogLikes.Remove(blogLike);
                _context.SaveChanges();
                return blogLike;
            }
            return null;
        }

        public IList<BlogLike>? GetAll()
        {
            if (_context.BlogLikes.Count() == 0)
                return null;
            return _context.BlogLikes.ToList();
        }

        public BlogLike? GetById(int key)
        {
            var blogLike = _context.BlogLikes.SingleOrDefault(b => b.BlogLikeId == key);
            return blogLike;
        }

        public BlogLike? Update(BlogLike blogLike)
        {
            var editBlogLike = GetById(blogLike.BlogLikeId);
            if (editBlogLike != null)
            {
                _context.Entry(editBlogLike).CurrentValues.SetValues(blogLike);
                _context.SaveChanges();
                return editBlogLike;
            }
            return null;
        }
    }
}
