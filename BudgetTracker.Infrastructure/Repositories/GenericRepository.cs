using BudgetTracker.Domain.Interfaces;
using BudgetTracker.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BudgetTracker.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataDbContext context;
        public GenericRepository(DataDbContext context)
        {
            this.context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await context.AddAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(Guid Id)
        {
            var entity = await GetByIdAsync(Id);

            context.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid Id)
        {
            return await context.Set<T>().FindAsync(Id);
        }

        public async Task<T> UpdateByIdAsync(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }
    }
}
