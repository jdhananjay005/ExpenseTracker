using ExpenseTracker.Data;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetAllExpenseDto>>GetAllAsync()
        {
            return await _context.Expenses
            .Include(e => e.User)
            .Select(e => new GetAllExpenseDto
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                Category = e.Category,
                ExpenseDate = e.ExpenseDate,
                CreatedAt  = e.CreatedAt,
                UserEmail = e.User.Email
            })
            .OrderByDescending(e => e.ExpenseDate)
            .ToListAsync();
            }

        public async Task<Expense?> GetExpenseByIdAsync(Guid id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }
        public async Task<GetExpenseByIdDto?> GetByIdAsync(Guid id)
        {
            return await _context.Expenses
                .Include(e => e.User)
                .Where(e => e.Id == id)
                .Select(e => new GetExpenseByIdDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Amount = e.Amount,
                    Category = e.Category,
                    ExpenseDate = e.ExpenseDate,
                    UserEmail = e.User.Email
                })
                .FirstOrDefaultAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
            return;
        }
    }
}
