namespace Budget.Domain.Shared
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent e) where TEvent : IEvent;
    }
}
