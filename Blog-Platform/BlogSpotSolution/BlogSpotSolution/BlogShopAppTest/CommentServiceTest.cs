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
    public class CommentServiceTest
    {
        private IRepository<string,User> userRepository;
        private IRepository<int, Blog> blogRepository;
        private IRepository<int, Category> categoryRepository;
        private IRepository<int, Comment> commentRepository;
        private IRepository<int, CommentLike> commentLikeRepository;
        private IRepository<int, BlogLike> blogLikeRepository;
        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<BlogSpotContext>()
                                .UseInMemoryDatabase("dbTestBlogger")
                                .Options;

            BlogSpotContext context = new BlogSpotContext(dbOptions);
            userRepository= new UserRepository(context);
            blogRepository = new BlogRepository(context);
            commentLikeRepository = new CommentLikeRepository(context);
            commentRepository = new CommentRepository(context);
            categoryRepository = new CategoryRepository(context);
            blogLikeRepository = new BlogLikeRepository(context);
        }

        [Test]
        [TestCase(1, "Test", "test")]
        [TestCase(2, "Newtest", "testing")]
        public void GetCommentsById(int id, string ue, string con)
        {
            // Arrange
            var appSettings = @"{""SecretKey"": ""Anything will work here this is just for testing""}";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            IBlogService blogService = new BlogService(blogRepository, categoryRepository, commentRepository, commentLikeRepository, blogLikeRepository);
            blogService.AddPost(new Blog
            {
                UserEmail = ue,
                Title = "new",
                Categories = new List<string> { "science", "stuff" },
                Content = "blog"
            });
            ICommentService commentService = new CommentService( blogRepository,userRepository,commentRepository,commentLikeRepository);
            commentService.AddComment(new Comment
            {
                UserEmail = ue,
                Content = con,
                BlogId = id,
                CommentedAt = DateTime.Now,
            });

            // Action
            var result = commentService.GetCommentsById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));

            // Check for null before accessing properties
            if (result[0] != null)
            {
                Assert.That(result[0].CommentId, Is.EqualTo(1));
            }
            else
            {
                Assert.Fail("Comment is null.");
            }
        }
    }
}
