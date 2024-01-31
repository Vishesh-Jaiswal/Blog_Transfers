using BlogSpotApp.Contexts;
using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;
using System.Reflection.Metadata;

namespace BlogSpotApp.Repositories
{
    public class CategoryRepository : IRepository<int,Category>
    {
        private readonly BlogSpotContext _context;

        public CategoryRepository(BlogSpotContext context)
        {
            _context = context;
        }

        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category? Delete(int key)
        {
            var category = GetById(key);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return category;
            }
            return null;
        }

        public IList<Category>? GetAll()
        {
            if (_context.Categories.Count() == 0)
                return null;
            return _context.Categories.ToList();
        }

        public Category? GetById(int key)
        {
            var blog = _context.Categories.SingleOrDefault(b => b.RelationId == key);
            return blog;
        }

        public Category? Update(Category category)
        {
            var editcategory = GetById(category.RelationId);
            if (editcategory != null)
            {
                _context.Entry(editcategory).CurrentValues.SetValues(category);
                _context.SaveChanges();
                return editcategory;
            }
            return null;
        }
    }
}
