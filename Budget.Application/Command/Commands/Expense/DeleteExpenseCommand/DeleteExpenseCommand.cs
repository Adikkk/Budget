using Budget.Application.Command.Abstractions;

namespace Budget.Application.Command.Commands.Expense.DeleteExpenseCommand
{
    public class DeleteExpenseCommand : ICommand<DeleteExpenseCommandResult>
    {
        public DeleteExpenseCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
