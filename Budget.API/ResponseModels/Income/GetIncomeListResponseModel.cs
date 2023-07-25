namespace Budget.API.ResponseModels.Income
{
    public class GetIncomeListResponseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid Id { get; set; }
    }
}
