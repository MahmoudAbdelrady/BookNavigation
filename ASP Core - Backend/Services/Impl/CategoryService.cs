using aspcorebackend.Dtos;
using aspcorebackend.Models;
using aspcorebackend.Repositories;
using aspcorebackend.Utils.Response;
using AutoMapper;

namespace aspcorebackend.Services.Impl {
    public class CategoryService : ICategoryService {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public Response<object> GetAllCategories(int? page) {
            try {
                List<Category> categories = categoryRepository.GetAllCategories(page);
                List<CategoryDTO> categoryDTOs = categories.Select(c => {
                    c.Books = [];
                    return mapper.Map<CategoryDTO>(c);
                }).ToList();
                return ResponseMaker<object>.SuccessRes("Categories retrieved successfully", categoryDTOs);
            } catch (Exception) {
                throw new Exception("Failed to retrieve categories");
            }
        }

        public Response<object> GetCategoryById(int id) {
            try {
                Category? category = categoryRepository.GetCategoryWithBooksById(id);
                if (category == null) {
                    return ResponseMaker<object>.ErrorRes("Category not found", null);
                }
                CategoryDTO categoryDTO = mapper.Map<CategoryDTO>(category);
                return ResponseMaker<object>.SuccessRes("Category retrieved successfully", categoryDTO);
            } catch (Exception) {
                throw new Exception("Failed to retrieve the category");
            }
        }

        public Response<object> AddCategory(CategoryDTO categoryDTO) {
            try {
                Category category = new() {
                    Title = categoryDTO.Title,
                };
                category = categoryRepository.AddCategory(category);
                mapper.Map(category, categoryDTO);
                return ResponseMaker<object>.SuccessRes("Category added successfully", categoryDTO);
            } catch (Exception) {
                throw new Exception("Failed to save the category");
            }
        }

        public Response<object> UpdateCategory(int id, CategoryDTO categoryDTO) {
            try {
                Category? category = categoryRepository.GetCategoryWithBooksById(id);
                if (category == null) {
                    return ResponseMaker<object>.ErrorRes("Category not found", null);
                }
                category.Title = categoryDTO.Title;
                categoryRepository.UpdateCategory();
                categoryDTO = mapper.Map<CategoryDTO>(category);
                return ResponseMaker<object>.SuccessRes("Category information updated successfully", categoryDTO);
            } catch (Exception) {
                throw new Exception("Failed to update category information");
            }
        }

        public Response<object> DeleteCategory(int id) {
            try {
                Category? category = categoryRepository.GetCategoryWithBooksById(id);
                if (category == null) {
                    return ResponseMaker<object>.ErrorRes("Category not found", null);
                }
                categoryRepository.DeleteCategory(category);
                return ResponseMaker<object>.SuccessRes("Category deleted successfully", null);
            } catch (Exception) {
                throw new Exception("Failed to delete the category");
            }
        }
    }
}
