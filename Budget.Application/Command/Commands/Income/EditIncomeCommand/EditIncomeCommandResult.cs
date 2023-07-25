using Budget.Application.Command.Abstractions;

namespace Budget.Application.Command.Commands.Income.EditExpenseCommand
{
    public class EditIncomeCommandResult : CommandResult
    {
        public EditIncomeCommandResult()
        {
            Success = false;
        }

        public EditIncomeCommandResult(bool success)
        {
            Success = success;
        }
    }
}
