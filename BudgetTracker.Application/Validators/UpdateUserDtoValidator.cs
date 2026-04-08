using BudgetTracker.Application.Dto.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Application.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(50).WithMessage("Maximum length is 50");

            RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login cannot be empty.")
            .MaximumLength(50).WithMessage("Maximum length is 50");
        }
    }
}
