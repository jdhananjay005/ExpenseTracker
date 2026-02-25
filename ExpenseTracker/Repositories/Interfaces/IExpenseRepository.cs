using ExpenseTracker.DTOs;
using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Task AddAsync(Expense expense);

        Task<List<Expense>> GetByUserIdAsync(Guid userId);

        Task<Expense?> GetByIdAsync(Guid id);

        Task UpdateAsync(Expense expense);

        Task DeleteAsync(Guid id);
    }
}
