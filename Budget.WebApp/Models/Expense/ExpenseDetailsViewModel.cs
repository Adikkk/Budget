namespace Budget.WebApp.Models.Expense
{
    public class ExpenseDetailsViewModel
    {
        public ExpenseDetailsViewModel(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
