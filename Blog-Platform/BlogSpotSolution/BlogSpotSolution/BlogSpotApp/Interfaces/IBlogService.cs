using BlogSpotApp.Models;
using BlogSpotApp.Models.DTOs;

namespace BlogSpotApp.Interfaces
{
    public interface IBlogService
    {
        List<Blog> GetBlogs();
        Blog? AddPost(Blog blog);
        Blog DeletePost(Blog blog);
        Blog EditPost(Blog blog);
        Blog? GetBlogById(int id);
        List<Blog>? GetBlogByEmail(string email);
        Blog? ReportBlog(Blog blog);
        List<Blog>? ReportedBlogs();
        Blog? ApproveReportBlog(int id);
    }
}
