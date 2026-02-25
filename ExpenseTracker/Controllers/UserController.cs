using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserDto dto)
        {
            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email
            };

            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            await _repo.AddAsync(user);
            return Ok("User registered successfully");
        }


    }
}
