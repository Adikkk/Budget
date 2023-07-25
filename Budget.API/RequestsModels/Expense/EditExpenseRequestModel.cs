namespace Budget.API.RequestsModels.Expense
{
    public class EditExpenseRequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsActive { get; set; }
    }
}
