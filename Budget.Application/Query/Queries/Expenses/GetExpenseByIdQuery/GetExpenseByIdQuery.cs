using Budget.Application.Query.Abstractions;
using Budget.Application.Query.QueryModel.Expenses;

namespace Budget.Application.Query.Queries.Expenses.GetExpenseByIdQuery
{
    public class GetExpenseByIdQuery : IQuery<ExpenseDetailsQueryModel>
    {
        public GetExpenseByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
