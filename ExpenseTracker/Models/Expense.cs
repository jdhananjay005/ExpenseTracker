namespace ExpenseTracker.Models
{
    public class Expense
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }   

        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}
