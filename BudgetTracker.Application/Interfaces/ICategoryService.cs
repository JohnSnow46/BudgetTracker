using BudgetTracker.Application.Dto.Category;
using BudgetTracker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto category);
        Task DeleteCategoryAsync(Guid Id);
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> GetCategoryByIdAsync(Guid Id);
        Task<CategoryResponseDto> UpdateCategoryByIdAsync(Guid Id, UpdateCategoryDto category);
    }
}
