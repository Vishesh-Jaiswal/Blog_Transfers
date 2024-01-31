using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;

namespace BlogSpotApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<int, Category> _categoryRepository;
        public CategoryService(IRepository<int, Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category>? GetCategoriesByBlogId(int id)
        {
            var categories= _categoryRepository.GetAll()?.Where(c => c.BlogId == id).ToList();
            if(categories!=null)
                return categories;
            return null;
        }
    }
}