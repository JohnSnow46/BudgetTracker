using BudgetTracker.Application.Dto.User;
using FluentValidation;

namespace BudgetTracker.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(50).WithMessage("Maximum length is 50");

            RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login cannot be empty.")
            .MaximumLength(50).WithMessage("Maximum length is 50");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MaximumLength(100).WithMessage("Maximum length is 100")
                .MinimumLength(8).WithMessage("Minimum 8 characters")
                .Matches("[!@#$%^&*]").WithMessage("Must contain special characters like: !@#$%^&*")
                .Matches("\\d").WithMessage("Must contain one number");
        }
    }
}
