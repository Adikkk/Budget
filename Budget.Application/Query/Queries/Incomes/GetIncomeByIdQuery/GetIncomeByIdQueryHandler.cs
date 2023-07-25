using Budget.Application.Query.Abstractions;
using Budget.Application.Query.Queries.Expenses.GetExpenseByIdQuery;
using Budget.Application.Query.QueryModel.Expenses;
using Budget.Application.Query.QueryModel.Incomes;
using Budget.Domain.Expenses;
using Budget.Domain.Incomes;
using MongoDB.Driver;

namespace Budget.Application.Query.Queries.Incomes.GetIncomeByIdQuery
{
    public class GetIncomeByIdQueryHandler : IQueryHandler<GetIncomeByIdQuery, IncomeDetailsQueryModel>
    {
        private readonly IIncomeWriteOnlyRepository _repository;

        public GetIncomeByIdQueryHandler(IIncomeWriteOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IncomeDetailsQueryModel> HandleAsync(GetIncomeByIdQuery query)
        {
            try
            {
                var queryResult = await _repository.GetByIdAsync(query.Id);

                if (queryResult is null)
                    return new IncomeDetailsQueryModel();

                var result = new IncomeDetailsQueryModel()
                {
                    Incomes = queryResult
                };

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
