using BlogSpotApp.Contexts;
using BlogSpotApp.Models;
using BlogSpotApp.Repositories;
using BlogSpotApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using BlogSpotApp.Interfaces;

namespace BlogSpotAppTest
{
    [TestFixture]
    public class BlogServiceTest
    {
        private IRepository<int, Blog> blogRepository;
        private IRepository<int,Category> categoryRepository;
        private IRepository<int, Comment> commentRepository;
        private  IRepository<int, CommentLike> commentLikeRepository;
        private  IRepository<int, BlogLike> blogLikeRepository;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<BlogSpotContext>()
                                .UseInMemoryDatabase("dbTestBlogger")
                                .Options;

            BlogSpotContext context = new BlogSpotContext(dbOptions);
            blogRepository = new BlogRepository(context);
            categoryRepository = new CategoryRepository(context);
            commentLikeRepository = new CommentLikeRepository(context);
            commentRepository = new CommentRepository(context);
            blogLikeRepository = new BlogLikeRepository(context);
        }

        [Test]
        [TestCase(1, "Test", "test", "new")]
        [TestCase(2, "Newtest", "testing", "old")]
        public void GetBlogById(int id, string ue, string heading, string cont)
        {
            // Arrange
            var appSettings = @"{""SecretKey"": ""Anything will work here this is just for testing""}";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            IBlogService blogService = new BlogService(blogRepository, categoryRepository,commentRepository,commentLikeRepository,blogLikeRepository);
            blogService.AddPost(new Blog
            {
                UserEmail = ue,
                Title = heading,
                Categories = new List<string> { "science", "stuff" },
                Content = cont
            });

            // Action
            var result = blogService.GetBlogById(id);
            
            // Assert
            Assert.That(result?.BlogId, Is.EqualTo(id));
        }
    }
}
