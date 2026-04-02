using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Application.Dto.Budget
{
    public class BudgetResponseDto
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }

        public Guid UserId { get; set; }
    }
}
