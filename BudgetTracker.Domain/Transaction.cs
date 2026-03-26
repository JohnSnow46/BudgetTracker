using BudgetTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }

        public Guid CategoryId { get; set; }
        public Guid BudgetId { get; set; }

        public Category Category { get; set; }
        public Budget Budget { get; set; }
    }
}
