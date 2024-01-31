using BlogSpotApp.Contexts;
using BlogSpotApp.Interfaces;

using BlogSpotApp.Models;
using BlogSpotApp.Repositories;

namespace BlogSpotApp.Repositories
{

    public class CommentRepository : IRepository<int, Comment>
    {
        private readonly BlogSpotContext _context;

        public CommentRepository(BlogSpotContext context)
        {
            _context = context;
        }

        public Comment Add(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment? Delete(int key)
        {
            var comment = GetById(key);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return comment;
            }
            return null;
        }

        public IList<Comment>? GetAll()
        {
            if (_context.Comments.Count() == 0)
                return null;
            return _context.Comments.ToList();
        }

        public Comment? GetById(int key)
        {
            var comment = _context.Comments.SingleOrDefault(b => b.CommentId == key);
            return comment;
        }

        public Comment? Update(Comment comment)
        {
            var editComment = GetById(comment.CommentId);
            if (editComment != null)
            {
                _context.Entry(editComment).CurrentValues.SetValues(comment);
                _context.SaveChanges();
                return editComment;
            }
            return null;
        }
    }
}
