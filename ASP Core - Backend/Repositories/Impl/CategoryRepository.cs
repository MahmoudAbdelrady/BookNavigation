using aspcorebackend.Config;
using aspcorebackend.Models;
using Microsoft.EntityFrameworkCore;

namespace aspcorebackend.Repositories.Impl {
    public class CategoryRepository : ICategoryRepository {
        private readonly BookNavDbContext dbContext;
        public CategoryRepository(BookNavDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public List<Category> GetAllCategories(int? page) {
            if (page < 1 || page == null) page = 1;
            int skip = (int)((page - 1) * 10);
            List<Category> categories = dbContext.Categories.Skip(skip).Take(10).ToList();
            return categories;
        }

        public Category? GetCategoryById(int id) {
            Category? category = dbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            return category;
        }

        public Category? GetCategoryWithBooksById(int id) {
            Category? category = dbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category != null) {
                List<Book> books = dbContext.Books.Where(b => b.CategoryId == id).Include(b => b.Author).Skip(0).Take(10).ToList();
                category.Books = books;
            }

            return category;
        }

        public Category AddCategory(Category category) {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return category;
        }

        public void UpdateCategory() {
            dbContext.SaveChanges();
        }

        public void DeleteCategory(Category category) {
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
        }
    }
}
