namespace Budget.Domain.Shared
{
    public interface IPersistentConnection<T> : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        T CreateModel();
    }
}
