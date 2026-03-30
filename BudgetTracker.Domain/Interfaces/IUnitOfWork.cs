using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Budget> Budgets { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Transaction> Transactions { get; }
        IGenericRepository<User> Users { get; }
        Task<int> CompleteAsync();
    }
}
