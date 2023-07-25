using Budget.Application.Command.Abstractions;

namespace Budget.Application.Command.Commands.Expense.EditExpenseCommand
{
    public class EditExpenseCommandResult : CommandResult
    {
        public EditExpenseCommandResult()
        {
            Success = false;
        }

        public EditExpenseCommandResult(bool success)
        {
            Success = success;
        }
    }
}
