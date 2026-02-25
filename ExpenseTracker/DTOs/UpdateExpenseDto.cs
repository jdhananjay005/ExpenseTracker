namespace ExpenseTracker.DTOs
{
    public class UpdateExpenseDto
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
