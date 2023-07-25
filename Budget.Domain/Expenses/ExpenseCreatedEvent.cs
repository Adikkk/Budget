using Budget.Domain.Shared;

namespace Budget.Domain.Expenses
{
    public class ExpenseCreatedEvent : Event
    {
        public Expense Data { get; set; }
        public ExpenseCreatedEvent(Expense expense)
        {
            Data = expense;
            Name = (nameof(ExpenseCreatedEvent));
        }
    }
}
