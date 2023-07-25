namespace Budget.Application.Command.Abstractions
{
    public interface ICommandResult
    {
        bool Success { get; }
        DateTime Executed { get; }
    }
}
