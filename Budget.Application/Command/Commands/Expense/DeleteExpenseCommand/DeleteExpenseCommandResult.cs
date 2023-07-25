using Budget.Application.Command.Abstractions;

namespace Budget.Application.Command.Commands.Expense.DeleteExpenseCommand
{
    public class DeleteExpenseCommandResult : CommandResult
    {
        public DeleteExpenseCommandResult()
        {
            Success = false;
        }

        public DeleteExpenseCommandResult(bool success)
        {
            Success = success;
        }
    }
}
