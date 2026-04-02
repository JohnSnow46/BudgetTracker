using BudgetTracker.Application.Dto.Transaction;
using BudgetTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForBudgetAsync([FromQuery] Guid budgetId)
        {
            var transactions = await _transactionService.GetAllTransactionsForBudgetAsync(budgetId);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionDto dto)
        {
            var transaction = await _transactionService.CreateTransactionAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = transaction.Id }, transaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionAsync(Guid id)
        {
            await _transactionService.DeleteTransactionAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionAsync(Guid id, [FromBody] UpdateTransactionDto dto)
        {
            var transaction = await _transactionService.UpdateTransactionByIdAsync(id, dto);

            return Ok(transaction);
        }
    }
}
