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
    public class BlogLikeServiceTest
    {
        private  IRepository<int, BlogLike> blogLikeRepository;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<BlogSpotContext>()
                                .UseInMemoryDatabase("dbTestBlogger")
                                .Options;

            BlogSpotContext context = new BlogSpotContext(dbOptions);
            blogLikeRepository = new BlogLikeRepository(context);
        }

        [Test]
        [TestCase(1,"Test")]
        [TestCase(2,"Newtest")]
        public void BlogLikeStatus(int id, string ue)
        {
            // Arrange
            var appSettings = @"{""SecretKey"": ""Anything will work here this is just for testing""}";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            IBlogLikeService bloglikeService = new BlogLikeService( blogLikeRepository);
            bloglikeService.BlogLikeToggle(new BlogLike
            {
                UserEmail = ue,
                BlogId = id,
            });

            // Action
            var result = bloglikeService.BlogLikeStatus(new BlogLike { UserEmail = "Test", BlogId = id });
            
            // Assert
            Assert.That(result?.BlogId, Is.EqualTo(id));
            Assert.That(result?.UserEmail, Is.EqualTo(ue));
        }
    }
}
