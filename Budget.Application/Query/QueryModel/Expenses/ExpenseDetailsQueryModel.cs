using Budget.Application.Query.Abstractions;
using Budget.Domain.Expenses;

namespace Budget.Application.Query.QueryModel.Expenses
{
    public class ExpenseDetailsQueryModel : IQueryModel
    {
        public Expense Expense { get; set; }
    }
}
