using Budget.Domain.Expenses;
using Budget.Domain.Shared;

namespace Budget.Domain.Incomes
{
    public class Income : IAggregateRoot
    {
        protected Income()
        {
        }

        private Income(
            string name,
            string description,
            string category,
            double amount,
            DateTime paymentDate,
            bool isActive
            )
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Category = category;
            Amount = amount;
            PaymentDate = paymentDate;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsActive = isActive;
        }

        private Income(
            Guid id,
            string name,
            string description,
            string category,
            double amount,
            DateTime paymentDate,
            bool isActive
        )
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            Amount = amount;
            PaymentDate = paymentDate;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public static Income CreateNew(
            string name,
            string description,
            string category,
            double amount,
            DateTime paymentDate,
            bool isActive
            )
        {
            return new Income(name, description, category, amount, paymentDate, isActive);
        }

        public static Income EditMainParameters(
            Guid id,
            string name,
            string description,
            string category,
            double amount,
            DateTime paymentDate,
            bool isActive
            )
        {
            return new Income(id, name, description, category, amount, paymentDate, isActive);
        }
    }
}
