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
    public class CommentLikeServiceTest
    {
        private  IRepository<int, CommentLike> commentLikeRepository;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<BlogSpotContext>()
                                .UseInMemoryDatabase("dbTestBlogger")
                                .Options;

            BlogSpotContext context = new BlogSpotContext(dbOptions);
            commentLikeRepository = new CommentLikeRepository(context);
        }

        [Test]
        [TestCase(1,1,"Test")]
        [TestCase(2,1,"Newtest")]
        public void CommentLikeStatus(int commId,int id, string ue)
        {
            // Arrange
            var appSettings = @"{""SecretKey"": ""Anything will work here this is just for testing""}";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            ICommentLikeService commentLikeService = new CommentLikeService( commentLikeRepository);
            commentLikeService.CommentLikeToggle(new CommentLike
            {
                UserEmail = ue,
                BlogId = id,
                CommentId = commId
            });

            // Action
            var result = commentLikeService.CommentLikeStatus(new CommentLike { UserEmail = "Test", BlogId = id,CommentId=commId });
            
            // Assert
            Assert.That(result?.BlogId, Is.EqualTo(id));
            Assert.That(result?.UserEmail, Is.EqualTo(ue));
            Assert.That(result?.CommentId, Is.EqualTo(commId));
        }
    }
}
