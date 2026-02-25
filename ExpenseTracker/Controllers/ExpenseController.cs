using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _repo;

        public ExpenseController(IExpenseRepository repo)
        {
            _repo = repo;
        }

        private Guid GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new UnauthorizedAccessException();

            return Guid.Parse(claim.Value);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseDto dto)
        {
            var userId = GetUserId();

            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = dto.Title,
                Amount = Math.Round(dto.Amount, 2),
                Category = dto.Category,
                ExpenseDate = dto.ExpenseDate,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(expense);

            return Ok("Expense added successfully");
        }

        // READ ALL (User Specific)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();

            var expenses = await _repo.GetByUserIdAsync(userId);

            return Ok(expenses);
        }

        // READ BY ID (Ownership Check)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();

            var expense = await _repo.GetByIdAsync(id);

            if (expense == null || expense.UserId != userId)
                return Unauthorized();

            return Ok(expense);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateExpenseDto dto)
        {
            var userId = GetUserId();

            var expense = await _repo.GetByIdAsync(id);

            if (expense == null || expense.UserId != userId)
                return Unauthorized();

            expense.Title = dto.Title;
            expense.Amount = Math.Round(dto.Amount, 2);
            expense.Category = dto.Category;
            expense.ExpenseDate = dto.ExpenseDate;

            await _repo.UpdateAsync(expense);

            return Ok("Expense updated successfully");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();

            var expense = await _repo.GetByIdAsync(id);

            if (expense == null || expense.UserId != userId)
                return Unauthorized();

            await _repo.DeleteAsync(id);

            return Ok("Expense deleted successfully");
        }
    }

}
