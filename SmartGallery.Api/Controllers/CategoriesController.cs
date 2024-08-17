using Microsoft.AspNetCore.Mvc;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.CategoryDtos;

namespace SmartGallery.Api.Controllers
{

    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            var data = result.GetData<IReadOnlyList<CategoryDto>>();
            return Ok(data);
        }
    }
}
