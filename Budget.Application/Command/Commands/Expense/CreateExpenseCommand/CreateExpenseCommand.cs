﻿using Budget.Application.Command.Abstractions;

namespace Budget.Application.Command.Commands.Expense.CreateExpenseCommand
{
    public class CreateExpenseCommand : ICommand<CreateExpenseCommandResult>
    {
        public CreateExpenseCommand(
            string name,
            string description,
            string category,
            double amount,
            DateTime paymentDate,
            bool isActive
        )
        {
            Name = name;
            Description = description;
            Category = category;
            Amount = amount;
            PaymentDate = paymentDate;
            IsActive = isActive;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsActive { get; set; }
    }
}
