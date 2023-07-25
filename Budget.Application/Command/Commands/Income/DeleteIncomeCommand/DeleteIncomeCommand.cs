using Budget.Application.Command.Abstractions;

namespace Budget.Application.Command.Commands.Income.DeleteExpenseCommand
{
    public class DeleteIncomeCommand : ICommand<DeleteIncomeCommandResult>
    {
        public DeleteIncomeCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
