using Budget.Domain.Expenses;
using Budget.Domain.Incomes;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Persistence.Repository
{
    public class IncomeWriteOnlyRepository : IIncomeWriteOnlyRepository
    {
        private readonly WriteDbContext writeDbContext;

        public IncomeWriteOnlyRepository(WriteDbContext writeDbContext)
        {
            this.writeDbContext = writeDbContext ?? throw new ArgumentNullException(nameof(writeDbContext));
        }

        public async Task<bool> Add(Income entity)
        {
            writeDbContext.Incomes.Add(entity);
            return await writeDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Income entity)
        {
            writeDbContext.Incomes.Remove(entity);
            return await writeDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Income>> GetAllAsync()
        {
            return await writeDbContext
                .Incomes
                .ToListAsync();
        }

        public async Task<Income> GetByIdAsync(Guid id)
        {
            return await writeDbContext
                .Incomes
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Income entity)
        {
            var income = await writeDbContext
                .Incomes
                .Where(x => x.Id == entity.Id)
                .FirstOrDefaultAsync();

            income.Name = entity.Name;
            income.Description = entity.Description;
            income.Category = entity.Category;
            income.Amount = entity.Amount;
            income.CreatedAt = entity.CreatedAt;
            income.UpdatedAt = DateTime.Now;
            income.IsActive = entity.IsActive;

            writeDbContext
                .Incomes
                .Update(income);

            return await writeDbContext.SaveChangesAsync() > 0;
        }
    }
}
