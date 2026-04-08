using BudgetTracker.Application.Dto.Transaction;
using FluentValidation;

namespace BudgetTracker.Application.Validators
{
    public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be more then 0.")
                .PrecisionScale(18, 2, false).WithMessage("Bad precision");

            RuleFor(x => x.Date)
                .NotEqual(DateTime.MinValue).WithMessage("Date shouldn't be default value");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Maximum 500 characters");

            RuleFor(x => x.BudgetId)
                .NotEqual(Guid.Empty).WithMessage("Budget Id shouldn't be default value");

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty).WithMessage("Category Id shouldn't be default value");
        }
    }
}
