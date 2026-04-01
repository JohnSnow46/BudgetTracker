using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BudgetTracker.Domain.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateByIdAsync(T entity);
        Task<T?> GetByIdAsync(Guid Id);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(Guid Id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
