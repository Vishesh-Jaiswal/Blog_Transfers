using BlogSpotApp.Models;

namespace BlogSpotApp.Interfaces
{
    public interface IBlogLikeService
    {
        BlogLike? BlogLikeStatus(BlogLike blogLike);
        BlogLike? BlogLikeToggle(BlogLike blogLike);
    }
}
