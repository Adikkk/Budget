namespace Budget.Domain.Shared
{
    public interface IEvent
    {
        Guid Id { get; set; }
        string Name { get; set; }
        DateTime OccurredOn { get; set; }
    }
}
