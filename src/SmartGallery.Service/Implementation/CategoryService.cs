using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Core.Repositories;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.CategoryDtos;

namespace SmartGallery.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Result> CreateCategoryAsync(CategoryForCreateDto category)
        {
            var categoryEntity = new Category 
            { 
                Name = category.Name
            };
            await _repositoryManager.CategoryRepository.CreateAsync(categoryEntity);
            await _repositoryManager.SaveChangesAsync();
            var categoryDto = new CategoryDto(categoryEntity.Id, categoryEntity.Name);
            return Result<CategoryDto>.Success(categoryDto);
           
        }

        public async Task<Result> DeleteCategoryAsync(int id)
        {
            var category = await _repositoryManager.CategoryRepository.GetByIdAsync(id);
            if (category is null)
                return ApplicationErrors.BadRequestError;
            _repositoryManager.CategoryRepository.Delete(category);
            await _repositoryManager.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> GetAllCategoriesAsync()
        {
            var categories = await _repositoryManager.CategoryRepository.GetAllAsync();
            var data = categories.Select(c => new CategoryDto(c.Id, c.Name)).ToList();
            return Result<IReadOnlyList<CategoryDto>>.Success(data);
        }

        public async Task<Result> GetCategoryByIdAsync(int id)
        {
            var category = await _repositoryManager.CategoryRepository.GetByIdAsync(id);
            if (category is null)
                return ApplicationErrors.NotFoundError;
            var categoryDto = new CategoryDto(category.Id, category.Name);
            return Result<CategoryDto>.Success(categoryDto);
        }

        public async Task<Result> UpdateCategoryAsync(int id, CategoryForUpdateDto category)
        {
            var categoryFromDB = await _repositoryManager.CategoryRepository.GetByIdAsync(id);
            if (categoryFromDB is null)
                return ApplicationErrors.BadRequestError;
            categoryFromDB.Name = category.Name;
            await _repositoryManager.SaveChangesAsync();
            return Result.Success();
        }
    }
}
