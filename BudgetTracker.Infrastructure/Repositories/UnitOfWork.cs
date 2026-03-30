using BudgetTracker.Domain;
using BudgetTracker.Domain.Interfaces;
using BudgetTracker.Infrastructure.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDbContext context;

        public IGenericRepository<Budget> Budgets { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Transaction> Transactions { get; private set; }
        public IGenericRepository<User> Users { get; private set; }

        public UnitOfWork(DataDbContext context)
        {
            this.context = context;
            Budgets = new GenericRepository<Budget>(context);
            Categories = new GenericRepository<Category>(context);
            Transactions = new GenericRepository<Transaction>(context);
            Users = new GenericRepository<User>(context);
        }

        public async Task<int> CompleteAsync() => await context.SaveChangesAsync();

        public void Dispose() => context.Dispose();
    }
}
