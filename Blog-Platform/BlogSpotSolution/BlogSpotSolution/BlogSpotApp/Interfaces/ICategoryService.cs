using BlogSpotApp.Models;

namespace BlogSpotApp.Interfaces
{
    public interface ICategoryService
    {
        List<Category>? GetCategoriesByBlogId(int id);
    }
}
