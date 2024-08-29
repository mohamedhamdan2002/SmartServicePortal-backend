using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.CategoryDtos;

namespace SmartGallery.Api.Controllers
{
    //[Authorize]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            var data = result.GetData<IReadOnlyList<CategoryDto>>();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return HandleResult<CategoryDto>(result);
        }

        [HttpPost]
        
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryForCreateDto category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);
            return HandleResult<CategoryDto>(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryForUpdateDto category)
        {
            var result =  await _categoryService.UpdateCategoryAsync(id, category);
            if (result.IsFailure)
                return HandleError(result.Error);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (result.IsFailure)
                return HandleError(result.Error);
            return NoContent();
        }
    }
}
