using Budget.Application.Command.Abstractions;

namespace Budget.Application.Command.Commands.Income.CreateIncomeCommand
{
    public class CreateIncomeCommandResult : CommandResult
    {
        public CreateIncomeCommandResult()
        {
            Success = false;
        }

        public CreateIncomeCommandResult(
            Guid id,
            string name,
            string description,
            string category,
            double amount,
            DateTime paymentDate,
            bool success
            )
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            Amount = amount;
            PaymentDate = paymentDate;
            Success = success;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
