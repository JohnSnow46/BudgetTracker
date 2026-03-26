using BudgetTracker.Domain.Interfaces;
using BudgetTracker.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Guid Id)
        {
            var entity = await GetByIdAsync(Id);
            ArgumentNullException.ThrowIfNull(entity);

            context.Remove(entity);
            await context.SaveChangesAsync();
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
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
