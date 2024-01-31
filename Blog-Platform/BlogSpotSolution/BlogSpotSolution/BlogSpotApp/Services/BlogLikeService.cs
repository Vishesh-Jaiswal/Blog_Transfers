using BlogSpotApp.Exceptions;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;

namespace BlogSpotApp.Services
{
    public class BlogLikeService : IBlogLikeService
    {
        private readonly IRepository<int, BlogLike> _blogLikeRepository;
        public BlogLikeService(IRepository<int, BlogLike> blogLikeRepository)
        {
            _blogLikeRepository = blogLikeRepository;
        }
        public BlogLike? BlogLikeStatus(BlogLike blogLike)
        {
            var existingLike = _blogLikeRepository.GetAll()?.FirstOrDefault(l => l.UserEmail == blogLike.UserEmail && l.BlogId == blogLike.BlogId);
            if (existingLike!=null)
            {
                return existingLike;
            }
            return null;
        }
        public BlogLike? BlogLikeToggle(BlogLike blogLike)
        {
            var newId = BlogLikeStatus(blogLike);
            if (newId == null)
            {
                var addResult = _blogLikeRepository.Add(blogLike);
                return addResult;
            }
            else if(newId != null)
            {
                var delResult = _blogLikeRepository.Delete(newId.BlogLikeId);
                return delResult;
            }
            throw new AlreadyLiked();
        }
    }
}