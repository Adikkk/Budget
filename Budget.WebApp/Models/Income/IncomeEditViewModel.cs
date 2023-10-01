namespace Budget.WebApp.Models.Income
{
    public class IncomeEditViewModel
    {
        public IncomeEditViewModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
