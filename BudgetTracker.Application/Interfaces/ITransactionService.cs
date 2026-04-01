using BudgetTracker.Application.Dto.Category;
using BudgetTracker.Application.Dto.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Application.Interfaces
{
    public interface ITransactionService
    {
            
        Task<TransactionResponseDto> CreateTransactionAsync(CreateTransactionDto transaction);
        Task DeleteTransactionAsync(Guid Id);
        Task<IEnumerable<TransactionResponseDto>> GetAllTransactionsForBudgetAsync(Guid UserId);
        Task<TransactionResponseDto> GetTransactionByIdAsync(Guid Id);
        Task<TransactionResponseDto> UpdateTransactionByIdAsync(Guid Id, UpdateTransactionDto transaction);
    }
}