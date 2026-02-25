using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _repo;

        public ExpenseController(IExpenseRepository repo)
        {
            _repo = repo;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseDto dto)
        {
            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                Title = dto.Title,
                Amount = Math.Round(dto.Amount, 2),
                Category = dto.Category,
                ExpenseDate = dto.ExpenseDate,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(expense);
            return Ok("Expense added");
        }

        // READ ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var expenses = await _repo.GetAllAsync();
            return Ok(expenses);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var expense = await _repo.GetByIdAsync(id);
            if (expense == null) return NotFound();
            return Ok(expense);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateExpenseDto dto)
        {
            var expense = await _repo.GetExpenseByIdAsync(id); 

            if (expense == null)
                return NotFound();

            expense.Title = dto.Title;
            expense.Amount = Math.Round(dto.Amount, 2);
            expense.Category = dto.Category;
            expense.ExpenseDate = dto.ExpenseDate;

            await _repo.UpdateAsync(expense);
            return Ok("Expense updated");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var expense = await _repo.GetExpenseByIdAsync(id);

            if (expense == null)
                return NotFound("Expense not found");

            await _repo.DeleteAsync(id);
            return Ok("Expense deleted successfully");
        }
    }

}
