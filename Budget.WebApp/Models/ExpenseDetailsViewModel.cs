namespace Budget.WebApp.Models
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
