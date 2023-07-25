using Budget.Domain.Expenses;
using Budget.Domain.Incomes;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Persistence
{
    public class WriteDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
            .ToTable("Expenses");

            modelBuilder.Entity<Income>()
                .ToTable("Incomes");
        }
    }
}
