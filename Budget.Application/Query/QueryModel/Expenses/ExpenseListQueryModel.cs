using Budget.Application.Query.Abstractions;
using Budget.Domain.Expenses;

namespace Budget.Application.Query.QueryModel.Expenses
{
    public class ExpenseListQueryModel : IQueryModel
    {
        public List<Expense> Expenses { get; set; }
    }
}
