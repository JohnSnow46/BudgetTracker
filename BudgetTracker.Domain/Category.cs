using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
