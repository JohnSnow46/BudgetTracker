using BudgetTracker.Application.Dto.Transaction;
using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain;
using BudgetTracker.Domain.Interfaces;


namespace BudgetTracker.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TransactionResponseDto> CreateTransactionAsync(CreateTransactionDto transactionDto)
        {
            if (transactionDto == null)
            {
                throw new ArgumentNullException(nameof(transactionDto));
            }

            var transaction = new BudgetTracker.Domain.Transaction
            {
                Id = Guid.NewGuid(),
                Amount = transactionDto.Amount,
                Date = transactionDto.Date,
                Type = transactionDto.Type,
                Description = transactionDto.Description,

                CategoryId = transactionDto.CategoryId,
                BudgetId = transactionDto.BudgetId
            };

            var budget = await _unitOfWork.Budgets.GetByIdAsync(transactionDto.BudgetId);
            if(budget == null)
            {
                throw new KeyNotFoundException(nameof(budget));
            }

            budget.AddTransaction(transaction);

            await _unitOfWork.Transactions.CreateAsync(transaction);
            await _unitOfWork.CompleteAsync();

            return new TransactionResponseDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type,
                Description = transaction.Description,

                CategoryId = transaction.CategoryId,
                BudgetId = transaction.BudgetId
            };
        }

        public async Task DeleteTransactionAsync(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            await _unitOfWork.Transactions.DeleteAsync(Id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetAllTransactionsForBudgetAsync(Guid budgetId)
        {
            if(budgetId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(budgetId));
            }
            var transactions = await _unitOfWork.Transactions.FindAsync(t => t.BudgetId == budgetId);
            
            if(transactions == null)
            {
                throw new KeyNotFoundException(nameof(transactions));
            }

            return transactions.Select(t => new TransactionResponseDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Date = t.Date,
                Type = t.Type,
                Description = t.Description,

                CategoryId = t.CategoryId,
                BudgetId = t.BudgetId
            });
        }

        public async Task<TransactionResponseDto> GetTransactionByIdAsync(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var transaction = await _unitOfWork.Transactions.GetByIdAsync(Id);

            if (transaction == null)
            {
                throw new KeyNotFoundException(nameof(transaction));
            }

            return new TransactionResponseDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type,
                Description = transaction.Description,

                CategoryId = transaction.CategoryId,
                BudgetId = transaction.BudgetId
            };
        }

        public async Task<TransactionResponseDto> UpdateTransactionByIdAsync(Guid Id, UpdateTransactionDto transactionDto)
        {
            if(transactionDto == null)
            {
                throw new ArgumentNullException(nameof(transactionDto));
            }
            if(Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var transaction = await _unitOfWork.Transactions.GetByIdAsync(Id);

            if(transaction == null)
            {
                throw new KeyNotFoundException(nameof(transaction));
            }

            transaction.Amount = transactionDto.Amount;
            transaction.Date = transactionDto.Date;
            transaction.Type = transactionDto.Type;
            transaction.Description = transactionDto.Description;

            transaction.CategoryId = transactionDto.CategoryId;

            await _unitOfWork.CompleteAsync();

            return new TransactionResponseDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type,
                Description = transaction.Description,

                CategoryId = transaction.CategoryId,
                BudgetId = transaction.BudgetId
            };
        }
    }
}
