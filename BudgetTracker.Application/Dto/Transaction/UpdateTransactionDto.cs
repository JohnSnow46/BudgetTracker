using BudgetTracker.Domain.Enums;

namespace BudgetTracker.Application.Dto.Transaction
{
    public class UpdateTransactionDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }
    }
}
