using Application.Dtos.CategoryDtos;
using Application.Services.Contracts;
using Application.Specifications;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;

namespace Application.Services.Implementation;

public class CategoryService : ICategoryService
{
    private readonly IRepositoryManager _repositoryManager;

    public CategoryService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task<Result<CategoryDto>> CreateCategoryAsync(CategoryForCreateDto category)
    {
        var categoryEntity = new Category
        {
            Name = category.Name
        };
        _repositoryManager.CategoryRepository.Create(categoryEntity);
        await _repositoryManager.SaveChangesAsync();
        var categoryDto = ToCategoryDto(categoryEntity);
        return Result.Success(categoryDto);

    }
    public async Task<Result> DeleteCategoriesAsync(CollectionOfIds categoriesIds)
    {
        await _repositoryManager.CategoryRepository.DeleteRangeAsync(categoriesIds.Values);
        return Result.Success();
    }
    public async Task<Result> DeleteCategoryAsync(int id)
    {
        var result = await GetCagetoryAndCheckIfExists(id);
        if (result.IsFailure) return result;
        _repositoryManager.CategoryRepository.Delete(result.Data);
        await _repositoryManager.SaveChangesAsync();
        return Result.Success();
    }
    public async Task<Result<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
    {
        var categories = await _repositoryManager.CategoryRepository.GetAllAsync();
        var data = categories.Select(category => ToCategoryDto(category));
        return Result.Success(data);
    }
    public async Task<Result<CategoryDto>> GetCategoryByIdAsync(int id)
    {
        var result = await GetCagetoryAndCheckIfExists(id, ApplicationErrors.NotFoundError);
        if (result.IsFailure) return Result.Fail<CategoryDto>(result.Error);
        var categoryDto = ToCategoryDto(result.Data);
        return Result.Success(categoryDto);
    }
    public async Task<Result> UpdateCategoryAsync(int id, CategoryForUpdateDto category)
    {
        var result = await GetCagetoryAndCheckIfExists(id);
        if (result.IsFailure) return result;
        result.Data.Name = category.Name;
        await _repositoryManager.SaveChangesAsync();
        return Result.Success();
    }
    private async Task<Result<Category>> GetCagetoryAndCheckIfExists(int id, Error? error = null)
    {
        var categoryDB = await _repositoryManager.CategoryRepository.GetByIdAsync(id);
        return categoryDB is null
            ? Result.Fail<Category>(error ?? ApplicationErrors.BadRequestError)
            : Result.Success(categoryDB!);
    }
    private CategoryDto ToCategoryDto(Category category)
        => new CategoryDto(category.Id, category.Name);
}
