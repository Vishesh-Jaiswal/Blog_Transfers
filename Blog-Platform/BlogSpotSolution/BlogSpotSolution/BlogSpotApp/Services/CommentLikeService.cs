using BlogSpotApp.Exceptions;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;

namespace BlogSpotApp.Services
{
    public class CommentLikeService : ICommentLikeService
    {
        private readonly IRepository<int, CommentLike> _commentLikeRepository;
        public CommentLikeService(IRepository<int, CommentLike> commentLikeRepository)
        {
            _commentLikeRepository = commentLikeRepository;
        }
        public CommentLike? CommentLikeStatus(CommentLike commentLike)
        {
            var existingLike = _commentLikeRepository.GetAll()?.FirstOrDefault(l => l.UserEmail == commentLike.UserEmail && l.CommentId == commentLike.CommentId);
            if (existingLike != null)
            {
                return existingLike;
            }
            return null;
        }
        public CommentLike? CommentLikeToggle(CommentLike commentLike)
        {
            var newId = CommentLikeStatus(commentLike);
            if (newId == null)
            {
                var result = _commentLikeRepository.Add(commentLike);
                return result;
            }
            else if (newId != null)
            {
                var delResult = _commentLikeRepository.Delete(newId.CommentLikeId);
                return delResult;
            }
            throw new AlreadyLiked();
        }

        public List<CommentLike>? CommentLikesByBlog(int blogId, string userEmail)
        {
            var getcommentLikes=_commentLikeRepository.GetAll()?.Where(cl=>cl.BlogId== blogId && cl.UserEmail==userEmail).ToList();
            return getcommentLikes;
        }
    }
}