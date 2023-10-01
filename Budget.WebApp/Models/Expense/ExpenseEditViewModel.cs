namespace Budget.WebApp.Models.Expense
{
    public class ExpenseEditViewModel
    {
        public ExpenseEditViewModel(Guid id) 
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
