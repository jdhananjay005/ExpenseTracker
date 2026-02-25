using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        
        Task<List<User>> GetAllAsync();

        Task<User?> GetByEmailAsync(string email);

    }
}
