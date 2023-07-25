using Budget.Domain.Shared;

namespace Budget.Domain.Expenses
{
    public interface IExpenseWriteOnlyRepository : IWriteOnlyRepository<Expense>
    {
        Task<bool> Add(Expense entity);
        Task<bool> Delete(Expense entity);
        Task<List<Expense>> GetAllAsync();
        Task<Expense> GetByIdAsync(Guid id);
        Task<bool> Update(Expense entity);
    }
}
