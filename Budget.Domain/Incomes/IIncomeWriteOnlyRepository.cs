using Budget.Domain.Shared;

namespace Budget.Domain.Incomes
{
    public interface IIncomeWriteOnlyRepository : IWriteOnlyRepository<Income>
    {
        Task<bool> Add(Income entity);
        Task<bool> Delete(Income entity);
        Task<List<Income>> GetAllAsync();
        Task<Income> GetByIdAsync(Guid id);
        Task<bool> Update(Income entity);
    }
}
