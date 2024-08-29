using SmartGallery.Core.Errors;
using SmartGallery.Service.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Service.Contracts
{
    public interface ICategoryService
    {
        Task<Result> GetAllCategoriesAsync();
        Task<Result> GetCategoryByIdAsync(int id);
        Task<Result> CreateCategoryAsync(CategoryForCreateDto category);
        Task<Result> UpdateCategoryAsync(int id, CategoryForUpdateDto category);
        Task<Result> DeleteCategoryAsync(int id);
    }
}
