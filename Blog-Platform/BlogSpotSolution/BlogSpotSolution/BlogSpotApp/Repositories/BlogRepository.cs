using BlogSpotApp.Models.DTOs;
using BlogSpotApp.Models;
using System.Security.Cryptography;
using System.Text;
using BlogSpotApp.Contexts;
using BlogSpotApp.Interfaces;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace BlogSpotApp.Repositories
{
    public class BlogRepository : IRepository<int, Blog>
    {
        private readonly BlogSpotContext _context;

        public BlogRepository(BlogSpotContext context)
        {
            _context = context;
        }

        public Blog Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return blog;
        }

        public Blog? Delete(int key)
        {
            var blog=GetById(key);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
                return blog;
            }
            return null;
        }


        public IList<Blog>? GetAll()
        {
            if (_context.Blogs.Count() == 0)
                return null;
            return _context.Blogs.ToList();
        }

        public Blog? GetById(int key)
        {
            var blog = _context.Blogs.SingleOrDefault(b => b.BlogId == key);
            return blog;
        }

        public Blog? Update(Blog blog)
        {
            var editBlog = GetById(blog.BlogId);
            if (editBlog != null)
            {
                _context.Entry(editBlog).CurrentValues.SetValues(blog);
                _context.SaveChanges();
                return editBlog;
            }
            return null;
        }
    }
}
