using aspcorebackend.Models;

namespace aspcorebackend.Repositories {
    public interface ICategoryRepository {
        List<Category> GetAllCategories(int? page);
        Category? GetCategoryById(int id);
        Category? GetCategoryWithBooksById(int id);
        Category AddCategory(Category category);
        void UpdateCategory();
        void DeleteCategory(Category category);
    }
}
