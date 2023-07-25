using Budget.Application.Query.Abstractions;
using Budget.Domain.Incomes;

namespace Budget.Application.Query.QueryModel.Incomes
{
    public class IncomeDetailsQueryModel : IQueryModel
    {
        public Income Incomes { get; set; }
    }
}
