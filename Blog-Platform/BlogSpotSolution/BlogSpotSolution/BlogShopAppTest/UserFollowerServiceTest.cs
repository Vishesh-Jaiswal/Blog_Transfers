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
    public class UserFollowerServiceTest
    {
        IRepository<string, User> userRepository;
        private IRepository<int, UserFollower> userFollowerRepository;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<BlogSpotContext>()
                                .UseInMemoryDatabase("dbTestBlogger") // Correct method name
                                .Options;
            BlogSpotContext context = new BlogSpotContext(dbOptions);
            userRepository = new UserRepository(context);
            userFollowerRepository = new UserFollowerRepository(context);
        }

        [Test]
        [TestCase("Test", "test123")]
        [TestCase("Test", "test321")]
        public void FollowTest(string un1, string un2)
        {
            // Arrange
            var appSettings = @"{""SecretKey"": ""Anything will work here this is just for testing""}";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var tokenService = new TokenService(configurationBuilder.Build());
            IUserFollowerService userFollowerService = new UserFollowerService(userFollowerRepository);
            userFollowerService.ToggleFollower(new UserFollower
            {
                FollowerId = un1,
                FollowingId = un2
            });

            // Action
            var result = userFollowerService.FollowStatus(new UserFollower {
                FollowerId = un1,
                FollowingId = un2
            });

            // Assert
            Assert.That(result?.FollowerId, Is.EqualTo(un1));
            Assert.That(result?.FollowingId, Is.EqualTo(un2));
        }
    }
}
