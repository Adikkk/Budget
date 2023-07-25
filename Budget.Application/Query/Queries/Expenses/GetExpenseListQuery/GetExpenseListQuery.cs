using Budget.Application.Query.Abstractions;
using Budget.Application.Query.QueryModel.Expenses;

namespace Budget.Application.Query.Queries.Expenses.GetExpenseListQuery
{
    public class GetExpenseListQuery : IQuery<ExpenseListQueryModel>
    {
        public Guid Id { get; set; }
    }
}
