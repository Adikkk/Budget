using Budget.Application.Query.Abstractions;
using Budget.Application.Query.QueryModel.Incomes;

namespace Budget.Application.Query.Queries.Incomes.GetIncomeByIdQuery
{
    public class GetIncomeByIdQuery : IQuery<IncomeDetailsQueryModel>
    {
        public GetIncomeByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
