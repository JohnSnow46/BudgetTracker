using BudgetTracker.Application.Dto.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Application.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(50).WithMessage("Maximum length is 50");
        }
    }
}
