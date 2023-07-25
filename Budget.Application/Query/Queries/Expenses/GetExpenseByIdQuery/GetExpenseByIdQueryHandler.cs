using Budget.Application.Query.Abstractions;
using Budget.Application.Query.QueryModel.Expenses;
using Budget.Domain.Expenses;
using MongoDB.Driver;

namespace Budget.Application.Query.Queries.Expenses.GetExpenseByIdQuery
{
    public class GetExpenseByIdQueryHandler : IQueryHandler<GetExpenseByIdQuery, ExpenseDetailsQueryModel>
    {
        //private readonly ReadDbContext readDbContext;
        private readonly IExpenseWriteOnlyRepository _repository;

        public GetExpenseByIdQueryHandler(IExpenseWriteOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ExpenseDetailsQueryModel> HandleAsync(GetExpenseByIdQuery query)
        {
            try
            {
                var queryResult = await _repository.GetByIdAsync(query.Id);

                if (queryResult is null)
                    return new ExpenseDetailsQueryModel();

                var result = new ExpenseDetailsQueryModel()
                {
                    Expense = queryResult
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
