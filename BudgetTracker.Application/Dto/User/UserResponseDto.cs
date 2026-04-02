using BudgetTracker.Application.Dto.Budget;


namespace BudgetTracker.Application.Dto.User
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }

        public BudgetResponseDto BudgetResponse { get; set; }
    }
}
