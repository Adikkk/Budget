namespace Budget.API.RequestsModels.Expense
{
    public class DeleteExpenseRequestModel
    {
        public DeleteExpenseRequestModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
