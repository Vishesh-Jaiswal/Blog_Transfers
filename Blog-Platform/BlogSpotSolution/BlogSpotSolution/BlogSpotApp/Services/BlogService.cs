using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using BlogSpotApp.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BlogSpotApp.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<int, Blog> _blogRepository;
        private readonly IRepository<int, Category> _categoryRepository;
        private readonly IRepository<int, Comment> _commentRepository;
        private readonly IRepository<int, CommentLike> _commentLikeRepository;
        private readonly IRepository<int, BlogLike> _blogLikeRepository;

        public BlogService(IRepository<int, Blog> blogRepository, IRepository<int, Category> categoryRepository,
            IRepository<int, Comment> commentRepository, IRepository<int, CommentLike> commentLikeRepository, IRepository<int, BlogLike> blogLikeRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _commentLikeRepository = commentLikeRepository;
            _blogLikeRepository = blogLikeRepository;
        }


        /// <summary>
        /// Add Blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>blog</returns>
        /// <exception cref="NoSuchUserExists"></exception>
        public Blog? AddPost(Blog blog)
        {
            string errorMessage = string.Empty;
            try
            {
                blog.CreationDate = DateTime.Now;
                var result = _blogRepository.Add(blog);
                AddCategoy(result.BlogId, blog.Categories);
                return result;
                
            }
            catch (NoSuchUserExists e) {
                errorMessage = e.Message;
            }
            return null;
        }

        public Category? AddCategoy(int blogId, List<string>? categories)
        {
            if(categories != null)
            foreach (var category in categories)
            {
                Category newCategory = new Category
                {
                    BlogId = blogId,
                    CategoryName = category
                };
                _categoryRepository.Add(newCategory);
            }
            return null;
        }



        public Blog EditPost(Blog blog)
        {
            var checkBlog=_blogRepository.GetAll()?.FirstOrDefault(b=>b.BlogId == blog.BlogId);
            if (checkBlog == null)
            {
                throw new BlogNotFoundException();
            }
            if(checkBlog.UserEmail != blog.UserEmail)
            {

                throw new UnauthorizedAccessException("You are not authorized to edit this blog post.");
            }
            blog.CreationDate = checkBlog.CreationDate;
            var result = _blogRepository.Update(blog);
            if (result == null)
            {
                throw new CouldNotEdit();
            }
            return blog;
        }

        public Blog? ReportBlog(Blog blog)
        {
            var blogToReport =_blogRepository.GetAll()?.FirstOrDefault(c => c.BlogId == blog.BlogId);
            if (blogToReport == null)
                return null;
            blogToReport.ReportReason = blog.ReportReason;
            blogToReport.ReportedBy = blog.ReportedBy;
            blogToReport.ReportedAt = DateTime.Now;
            var result = _blogRepository.Update(blogToReport);
            if (result == null)
            {
                throw new CouldNotEdit();
            }
            blogToReport.ReportedBlogs?.Add(blog);
            return blog;
        }

        public List<Blog>? ReportedBlogs()
        {
            var allBlogs = _blogRepository.GetAll();
            if (allBlogs == null)
                return null;
            List<Blog> reportedBlogs = allBlogs.Where(c => c.ReportedAt != null).ToList();
            if (reportedBlogs.Count == 0)
            {
                return null;
            }
            return reportedBlogs;
        }

        public Blog? ApproveReportBlog(int id)
        {
            var blogToApprove = _blogRepository.GetAll()?.FirstOrDefault(c => c.BlogId == id);
            if (blogToApprove == null)
                return null;
            blogToApprove.ReportReason = null;
            blogToApprove.ReportedBy = null;
            blogToApprove.ReportedAt = null;
            var result = _blogRepository.Update(blogToApprove);
            blogToApprove.ReportedBlogs?.Remove(blogToApprove);
            return blogToApprove;
        }

        public List<Blog> GetBlogs()
        {
            var blogs = _blogRepository.GetAll();
            if (blogs != null)
            {
                var unreportedBlogs = blogs.Where(c => c.ReportedAt == null);
                var sortedBlogs = unreportedBlogs.OrderByDescending(blog => blog.CreationDate).ToList();
                return sortedBlogs;
            }
            return new List<Blog>();
        }

        public Blog? GetBlogById(int id)
        {
            var blogs = _blogRepository.GetById(id);
            if (blogs == null)
            {
                return null;
            }

            return blogs;
        }

        public List<Blog>? GetBlogByEmail(string email)
        {
            var blogs = _blogRepository.GetAll()?.Where(c => c.UserEmail == email).ToList();
            if (blogs==null)
            {
                return new List<Blog>();
            }
     
            var sortedBlogs = blogs.OrderByDescending(blog => blog.CreationDate).ToList();
            return sortedBlogs;
        }

        /// <summary>
        /// Method for deleting blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>blog</returns>
        /// <exception cref="BlogNotFoundException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="CouldNotDelete"></exception>
        public Blog DeletePost(Blog blog)
        {
            var blogCheck = _blogRepository.GetAll()?.FirstOrDefault(b => b.BlogId == blog.BlogId);

            if (blogCheck == null)
            {
                throw new BlogNotFoundException();
            }
            if (blogCheck.UserEmail != blog.UserEmail)
            {

                throw new UnauthorizedAccessException("You are not authorized to delete this blog post.");
            }
            blogCheck.ReportedBlogs?.Remove(blog);
            if(DeleteBlogLikeCascade(blog.BlogId)==true && DeleteCommentCascade(blog.BlogId)==true && DeleteBlogCategoryCascade(blog)==true){
                var result = _blogRepository.Delete(blogCheck.BlogId);
                if (result == null)
                {
                    throw new CouldNotDelete();
                }
                return blog;
            }
            throw new CouldNotDelete();


        }

        private bool DeleteBlogCategoryCascade(Blog blog)
        {
            var checkCategories = _categoryRepository.GetAll()?.Where(b => b.BlogId == blog.BlogId).ToList();
            if (checkCategories == null)
            {
                return true;
            }
            if (checkCategories != null)
            {
                foreach (var category in checkCategories)
                {
                    _categoryRepository.Delete(category.RelationId);
                }
            }
            return true;
        }
        private bool DeleteBlogLikeCascade(int id)
        {
            var checkBlogLikes = _blogLikeRepository.GetAll()?.Where(b => b.BlogId == id);
            if (checkBlogLikes == null)
            {
                return true;
            }
            if (checkBlogLikes != null)
            {
                foreach (var item in checkBlogLikes)
                {
                    _blogLikeRepository.Delete(item.BlogLikeId);
                }
            }
            return true;
        }

        private bool DeleteCommentCascade(int id)
        {
            var checkBlogLikes = _commentRepository.GetAll()?.Where(b => b.BlogId == id);
            if (checkBlogLikes == null)
            {
                return true;
            }
            if (checkBlogLikes != null)
            {
                foreach (var item in checkBlogLikes)
                {
                    DeleteCommentLikeCascade(item.CommentId);
                    _commentRepository.Delete(id);
                }
            }
            return true;
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
                    var result=_commentLikeRepository.Delete(item.CommentLikeId);
                }
            }
            return true;
        }

    }
}
