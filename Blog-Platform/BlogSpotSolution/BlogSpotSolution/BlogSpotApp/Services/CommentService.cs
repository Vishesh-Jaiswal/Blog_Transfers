using BlogSpotApp.Exceptions;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;

namespace BlogSpotApp.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Blog> _blogRepository;
        private readonly IRepository<int, Comment> _commentRepository;
        private readonly IRepository<int, CommentLike> _commentLikeRepository;

        public CommentService(IRepository<int, Blog> blogRepository, IRepository<string, User> userRepository, IRepository<int, Comment> commentRepository, IRepository<int, CommentLike> commentLikeRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _commentLikeRepository = commentLikeRepository;
        }
        /// <summary>
        /// Add Comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        /// <exception cref="BlogNotFoundException"></exception>
        /// <exception cref="NoSuchUserExists"></exception>
        public Comment? AddComment(Comment comment)
        {
            var blogCheck = _blogRepository.GetAll()?.FirstOrDefault(c => c.BlogId == comment.BlogId);

            if (blogCheck == null)
            {
                throw new BlogNotFoundException();
            }
            comment.CommentedAt = DateTime.Now;
            var result=_commentRepository.Add(comment);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public Comment? EditComment(Comment comment)
        {
            var checkCommenter = _commentRepository.GetAll()?.SingleOrDefault(c => c.CommentId == comment.CommentId);
            if (checkCommenter==null || (checkCommenter.UserEmail != comment.UserEmail))
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this comment.");
            }
            comment.CommentedAt = checkCommenter.CommentedAt;
            var result = _commentRepository.Update(comment);
            if (result == null)
            {
                throw new CouldNotEdit();
            }
            return comment;
        }

        public Comment? ReportComment(Comment comment)
        {
            var commentToReport = _commentRepository.GetAll()?.FirstOrDefault(c => c.CommentId == comment.CommentId);
            if (commentToReport == null)
                return null;
            commentToReport.ReportReason= comment.ReportReason;
            commentToReport.ReportedBy= comment.ReportedBy;
            commentToReport.ReportedAt = DateTime.Now;
            var result = _commentRepository.Update(commentToReport);
            if (result == null)
            {
                throw new CouldNotEdit();
            }
            commentToReport.ReportedComments?.Add(comment);
            return comment;
        }

        public List<Comment>? ReportedComments()
        {
            var allComments = _commentRepository.GetAll();
            if (allComments==null)
                return null;
            List<Comment> reportedComments = allComments.Where(c => c.ReportedAt != null).ToList();
            if(reportedComments.Count == 0)
            {
                return null;
            }
            return reportedComments;
        }
        public Comment? ApproveReportComment(int id)
        {
            var commentToReport = _commentRepository.GetAll()?.FirstOrDefault(c => c.CommentId == id);
            if (commentToReport == null)
                return null;
            commentToReport.ReportReason = null;
            commentToReport.ReportedBy = null;
            commentToReport.ReportedAt = null;
            var result = _commentRepository.Update(commentToReport);
            commentToReport.ReportedComments?.Remove(commentToReport);
            return commentToReport;
        }
        public List<Comment>? GetCommentsById(int id)
        {
            var comments = _commentRepository.GetAll()?.Where(c => c.BlogId == id).ToList();
            if (comments==null)
            {
                return null;
            }
            var unreportedComments = comments.Where(c => c.ReportedAt == null);
            var sortedComments = unreportedComments.OrderByDescending(comment => comment.CommentedAt).ToList();
            return sortedComments;
        }
        
        public List<Comment>? GetCommentsByEmail(string email)
        {
            var comments = _commentRepository.GetAll()?.Where(c => c.UserEmail == email).ToList();
            if (comments == null)
            {
                return null;
            }
            var sortedComments = comments.OrderByDescending(comment => comment.CommentedAt).ToList();
            return sortedComments;
            
        }

        public Comment? DeleteComment(Comment comment)
        {
            var checkCommenter = _commentRepository.GetAll()?.SingleOrDefault(c => c.CommentId == comment.CommentId);
            if (checkCommenter == null || (checkCommenter.UserEmail != comment.UserEmail))
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this comment.");
            }
            checkCommenter.ReportedComments?.Remove(comment);
            comment.CommentedAt = checkCommenter.CommentedAt;
            if (DeleteCommentLikeCascade(comment.CommentId))
            {
                var result = _commentRepository.Delete(comment.CommentId);
                if (result == null)
                {
                    return null;
                }
                return comment;
            }
            return null;
        }

        private bool DeleteCommentLikeCascade(int commentID)
        {
            var getItems = _commentLikeRepository.GetAll()?.Where(cl => cl.CommentId == commentID);
            if (getItems == null)
            {
                return true;
            }
            if (getItems != null)
            {
                foreach (var item in getItems)
                {
                    var result = _commentLikeRepository.Delete(item.CommentLikeId);
                }
            }
            return true;
        }
    }
}
