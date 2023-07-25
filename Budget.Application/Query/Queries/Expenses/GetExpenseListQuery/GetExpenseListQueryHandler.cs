using Budget.Application.Query.Abstractions;
using Budget.Application.Query.QueryModel.Expenses;
using Budget.Domain.Expenses;

namespace Budget.Application.Query.Queries.Expenses.GetExpenseListQuery
{
    public class GetExpenseListQueryHandler : IQueryHandler<GetExpenseListQuery, ExpenseListQueryModel>
    {
        private readonly IExpenseWriteOnlyRepository _repository;

        public GetExpenseListQueryHandler(IExpenseWriteOnlyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ExpenseListQueryModel> HandleAsync(GetExpenseListQuery query)
        {
            try
            {
                var expenses = await _repository.GetAllAsync();

                var result = new ExpenseListQueryModel() { Expenses = expenses };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
