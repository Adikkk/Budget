namespace Budget.API.RequestsModels.Income
{
    public class DeleteIncomeRequestModel
    {
        public DeleteIncomeRequestModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
