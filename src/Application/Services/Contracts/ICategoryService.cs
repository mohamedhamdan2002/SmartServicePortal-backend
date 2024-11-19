using Application.Dtos.CategoryDtos;
using Application.Utilities;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface ICategoryService
{
    Task<Result<IEnumerable<CategoryDto>>> GetAllCategoriesAsync();
    Task<Result<CategoryDto>> GetCategoryByIdAsync(int id);
    Task<Result<CategoryDto>> CreateCategoryAsync(CategoryForCreateDto category);
    Task<Result> UpdateCategoryAsync(int id, CategoryForUpdateDto category);
    Task<Result> DeleteCategoryAsync(int id);
    Task<Result> DeleteCategoriesAsync(CollectionOfIds categoriesIds);
}
