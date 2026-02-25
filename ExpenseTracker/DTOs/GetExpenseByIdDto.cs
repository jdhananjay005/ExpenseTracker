namespace ExpenseTracker.DTOs
{
    public class GetExpenseByIdDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public string Category { get; set; }

        public DateTime ExpenseDate { get; set; }

        public string UserEmail { get; set; }
    }
}
