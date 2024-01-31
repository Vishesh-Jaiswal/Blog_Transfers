using BlogSpotApp.Models;

namespace BlogSpotApp.Interfaces
{
    public interface ICommentLikeService
    {
        CommentLike? CommentLikeStatus(CommentLike commentLike);
        CommentLike? CommentLikeToggle(CommentLike commentLike);
        List<CommentLike>? CommentLikesByBlog(int blogId, string userEmail);
    }
}
