using BudgetTracker.Application.Dto.Category;
using FluentValidation;

namespace BudgetTracker.Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(50).WithMessage("Maximum length is 50");
        }
    }
}
