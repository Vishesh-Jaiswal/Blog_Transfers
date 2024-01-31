using BlogSpotApp.Interfaces;
using BlogSpotApp.Models.DTOs;
using BlogSpotApp.Models;
using BlogSpotApp.Repositories;
using BlogSpotApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using BlogSpotApp.Contexts;

namespace BlogSpotAppTest
{
    public class UserServiceTest
    {
        IRepository<string, User> userRepository;
        private IRepository<int, Blog> blogRepository;
        private IRepository<int, Category> categoryRepository;
        private IRepository<int, Comment> commentRepository;
        private IRepository<int, CommentLike> commentLikeRepository;
        private IRepository<int, BlogLike> blogLikeRepository;
        private IRepository<int, UserFollower> userFollowerRepository;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<BlogSpotContext>()
                                .UseInMemoryDatabase("dbTestBlogger") // Correct method name
                                .Options;
            BlogSpotContext context = new BlogSpotContext(dbOptions);
            userRepository = new UserRepository(context);
            blogRepository = new BlogRepository(context);
            blogLikeRepository = new BlogLikeRepository(context);
            categoryRepository = new CategoryRepository(context);
            commentRepository = new CommentRepository(context);
            commentLikeRepository = new CommentLikeRepository(context);
            userFollowerRepository = new UserFollowerRepository(context);
        }

        [Test]
        [TestCase("Test", "test123")]
        [TestCase("Test", "test321")]
        public void LoginTest(string un, string pass)
        {
            // Arrange
            var appSettings = @"{""SecretKey"": ""Anything will work here this is just for testing""}";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var tokenService = new TokenService(configurationBuilder.Build());
            IUserService userService = new UserService(userRepository, tokenService, blogRepository, categoryRepository, commentRepository, commentLikeRepository, blogLikeRepository,  userFollowerRepository);
            userService.Register(new UserDTO
            {
                UserEmail = un,
                Password = pass,
                Role = "Blogger"
            });

            // Action
            var result = userService.Login(new UserDTO { UserEmail = "Test", Password = "test123" });

            // Assert
            Assert.That(result?.UserEmail, Is.EqualTo(un));
        }
    }
}
