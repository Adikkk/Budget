using Budget.Application.Command.Abstractions;
using Budget.Domain.Expenses;
using Budget.Domain.Shared;

namespace Budget.Application.Command.Commands.Expense.DeleteExpenseCommand
{
    public class DeleteExpenseCommandHandler : ICommandHandler<DeleteExpenseCommand, DeleteExpenseCommandResult>
    {
        private readonly IExpenseWriteOnlyRepository _expenseRepository;

        public DeleteExpenseCommandHandler(IExpenseWriteOnlyRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<DeleteExpenseCommandResult> Handle(DeleteExpenseCommand command)
        {
            var expenseToDelete = await _expenseRepository.GetByIdAsync(command.Id);

            var success = await _expenseRepository.Delete(expenseToDelete);

            return new DeleteExpenseCommandResult(success);
        }
    }
}
