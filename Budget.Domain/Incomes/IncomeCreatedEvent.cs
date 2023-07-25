using Budget.Domain.Shared;

namespace Budget.Domain.Incomes
{
    public class IncomeCreatedEvent : Event
    {
        public Income Data { get; set; }

        public IncomeCreatedEvent(Income income)
        {
            Data = income;
            Name = (nameof(IncomeCreatedEvent));
        }
    }
}
