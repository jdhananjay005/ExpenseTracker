namespace ExpenseTracker.DTOs
{
    public class CreateExpenseDto
    {
        public string Title { get; set; }

        public Guid UserId { get; set; }
        public decimal Amount { get; set; }

        public string Category { get; set; }

        public DateTime ExpenseDate { get; set; }
    }
}
