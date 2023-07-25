namespace Budget.Domain.Shared
{
    public interface IBuilder<T>
    {
        T Build();
    }
}
