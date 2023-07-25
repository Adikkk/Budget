using Budget.Application.Command.Abstractions;

namespace Budget.Application.Command.Commands.Income.DeleteExpenseCommand
{
    public class DeleteIncomeCommandResult : CommandResult
    {
        public DeleteIncomeCommandResult()
        {
            Success = false;
        }

        public DeleteIncomeCommandResult(bool success)
        {
            Success = success;
        }
    }
}
