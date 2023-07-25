using Budget.Domain.Expenses;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Persistence.Repository
{
    public class ExpenseWriteOnlyRepository : IExpenseWriteOnlyRepository
    {
        private readonly WriteDbContext writeDbContext;

        public ExpenseWriteOnlyRepository(WriteDbContext writeDbContext)
        {
            this.writeDbContext = writeDbContext ?? throw new ArgumentNullException(nameof(writeDbContext));
        }

        public async Task<bool> Add(Expense entity)
        {
            writeDbContext.Expenses.Add(entity);
            return await writeDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Expense entity)
        {
            writeDbContext.Expenses.Remove(entity);
            return await writeDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await writeDbContext
                .Expenses
                .ToListAsync();
        }

        public async Task<Expense> GetByIdAsync(Guid id)
        {
            return await writeDbContext
                .Expenses
                 .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Expense entity)
        {
            var expense = await writeDbContext
                .Expenses
                .Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            expense.Name = entity.Name;
            expense.Description = entity.Description;
            expense.Category = entity.Category;
            expense.Amount = entity.Amount;
            expense.CreatedAt = entity.CreatedAt;
            expense.UpdatedAt = DateTime.Now;
            expense.IsActive = entity.IsActive;

            writeDbContext
                .Expenses
                .Update(expense);

            return await writeDbContext.SaveChangesAsync() > 0;
        }
    }
}
