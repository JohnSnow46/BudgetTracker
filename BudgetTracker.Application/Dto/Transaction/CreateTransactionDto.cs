using BudgetTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Application.Dto.Transaction
{
    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }
        public Guid BudgetId { get; set; }
    }
}
