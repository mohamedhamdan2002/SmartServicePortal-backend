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
        public async Task<Result> GetAllCategoriesAsync()
        {
            var categories = await _repositoryManager.CategoryRepository.GetAllAsync();
            var data = categories.Select(c => new CategoryDto(c.Id, c.Name)).ToList();
            return Result<IReadOnlyList<CategoryDto>>.Success(data);
        }
    }
}
