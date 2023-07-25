using Budget.Application.Query.Abstractions;
using Budget.Application.Query.QueryModel.Incomes;
using Budget.Domain.Incomes;

namespace Budget.Application.Query.Queries.Incomes.GetIncomeListQuery
{
    public class GetIncomeListQueryHandler : IQueryHandler<GetIncomeListQuery, IncomeListQueryModel>
    {
        private readonly IIncomeWriteOnlyRepository _repository;

        public GetIncomeListQueryHandler(IIncomeWriteOnlyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IncomeListQueryModel> HandleAsync(GetIncomeListQuery query)
        {
            try
            {
                var incomes = await _repository.GetAllAsync();

                var result = new IncomeListQueryModel() { Incomes = incomes };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
