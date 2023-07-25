namespace Budget.Domain.Shared
{
    public interface IWriteOnlyRepository<TEntity> where TEntity : IAggregateRoot
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Guid id);

        Task<bool> Add(TEntity entity);

        Task<bool> Update(TEntity entity);

        Task<bool> Delete(TEntity entity);
    }
}
