using Budget.Application.Query.Abstractions;
using Budget.Application.Query.QueryModel.Incomes;

namespace Budget.Application.Query.Queries.Incomes.GetIncomeListQuery
{
    public class GetIncomeListQuery : IQuery<IncomeListQueryModel>
    {
        public Guid Id { get; set; }
    }
}
