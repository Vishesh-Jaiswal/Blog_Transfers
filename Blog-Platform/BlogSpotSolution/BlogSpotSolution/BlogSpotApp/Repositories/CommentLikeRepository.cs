using BlogSpotApp.Contexts;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using System.Reflection.Metadata;

namespace BlogSpotApp.Repositories
{
    public class CommentLikeRepository : IRepository<int, CommentLike>
    {
        private readonly BlogSpotContext _context;

        public CommentLikeRepository(BlogSpotContext context)
        {
            _context = context;
        }

        public CommentLike Add(CommentLike commentLike)
        {
            _context.CommentLikes.Add(commentLike);
            _context.SaveChanges();
            return commentLike;
        }

        public CommentLike? Delete(int key)
        {
            var commentLike = GetById(key);
            if (commentLike != null)
            {
                _context.CommentLikes.Remove(commentLike);
                _context.SaveChanges();
                return commentLike;
            }
            return null;
        }

        public IList<CommentLike>? GetAll()
        {
            if (_context.CommentLikes.Count() == 0)
                return null;
            return _context.CommentLikes.ToList();
        }

        public CommentLike? GetById(int key)
        {
            var commentLike = _context.CommentLikes.SingleOrDefault(b => b.CommentLikeId == key);
            return commentLike;
        }

        public CommentLike? Update(CommentLike commentLike)
        {
            var editCommentLike = GetById(commentLike.CommentLikeId);
            if (editCommentLike != null)
            {
                _context.Entry(editCommentLike).CurrentValues.SetValues(commentLike);
                _context.SaveChanges();
                return editCommentLike;
            }
            return null;
        }
    }
}
