using Api.Utilities;
using Application.Dtos.CategoryDtos;
using Application.Services.Contracts;
using Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
        return HandleResult<IReadOnlyList<CategoryDto>>(result);
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
        return HandleResult<CategoryDto>(result, ActionEnum.CreatedAtResult);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryForUpdateDto category)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, category);
        return HandleResult<Result>(result, ActionEnum.NoContentResult);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        return HandleResult<Result>(result, ActionEnum.NoContentResult);
    }
}
