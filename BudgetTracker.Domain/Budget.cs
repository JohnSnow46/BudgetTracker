using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Domain
{
    public class Budget
    {
        public Guid Id { get; set; }
        public decimal Balance { get; private set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
        public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

        public void AddTransaction(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction);

            Transactions.Add(transaction);
            Balance += transaction.Amount;
        }
    }
}
