using aspcorebackend.Dtos;
using aspcorebackend.Utils.Response;

namespace aspcorebackend.Services {
    public interface ICategoryService {
        Response<object> GetAllCategories(int? page);
        Response<object> GetCategoryById(int id);
        Response<object> AddCategory(CategoryDTO categoryDTO);
        Response<object> UpdateCategory(int id, CategoryDTO categoryDTO);
        Response<object> DeleteCategory(int id);
    }
}
