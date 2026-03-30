using BudgetTracker.Application.Dto.Category;
using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain;
using BudgetTracker.Domain.Interfaces;

namespace BudgetTracker.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto));
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryDto.Name
            };

            await _unitOfWork.Categories.CreateAsync(category);
            await _unitOfWork.CompleteAsync();

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task DeleteCategoryAsync(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            await _unitOfWork.Categories.DeleteAsync(Id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            return categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(Id);
            if(category == null)
            {
                throw new KeyNotFoundException(nameof(category));
            }

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryResponseDto> UpdateCategoryByIdAsync(Guid Id, UpdateCategoryDto categoryDto)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto);
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(Id);
            if (category == null)
            {
                throw new KeyNotFoundException(nameof(category));
            }

            category.Name = categoryDto.Name;

            await _unitOfWork.CompleteAsync();

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
