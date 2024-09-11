using Application.Dtos.CategoryDtos;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface ICategoryService
{
    Task<Result> GetAllCategoriesAsync();
    Task<Result> GetCategoryByIdAsync(int id);
    Task<Result> CreateCategoryAsync(CategoryForCreateDto category);
    Task<Result> UpdateCategoryAsync(int id, CategoryForUpdateDto category);
    Task<Result> DeleteCategoryAsync(int id);
}
