namespace Budget.WebApp.Models.Income
{
    public class IncomeDetailsViewModel
    {
        public IncomeDetailsViewModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
