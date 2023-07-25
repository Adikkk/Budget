using Budget.Application.Query.Abstractions;
using Budget.Domain.Incomes;

namespace Budget.Application.Query.QueryModel.Incomes
{
    public class IncomeListQueryModel : IQueryModel
    {
        public List<Income> Incomes { get; set; }
    }
}
