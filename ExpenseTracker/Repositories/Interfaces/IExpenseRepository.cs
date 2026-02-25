using ExpenseTracker.DTOs;
using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Task AddAsync(Expense expense);
        Task<List<GetAllExpenseDto>> GetAllAsync();
        Task<Expense?> GetExpenseByIdAsync(Guid id);
        Task DeleteAsync(Guid id);

        Task<GetExpenseByIdDto?> GetByIdAsync(Guid id);
        Task UpdateAsync(Expense expense);
    }
}
