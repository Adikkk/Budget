namespace Budget.Application.Query.Abstractions
{
    public interface IQueryParameters<TQuery> where TQuery : IQuery
    {
        T GetParameters<T>(TQuery model);
    }
}
