using Budget.Application.Command.Abstractions;
using Budget.Domain.Expenses;
using Budget.Domain.Shared;

namespace Budget.Application.Command.Commands.Expense.EditExpenseCommand
{
    public class EditExpenseCommandHandler : ICommandHandler<EditExpenseCommand, EditExpenseCommandResult>
    {
        private readonly IExpenseWriteOnlyRepository expenseRepository;
        private readonly ValidationNotificationHandler notificationHandler;

        public EditExpenseCommandHandler(IExpenseWriteOnlyRepository expenseRepository, ValidationNotificationHandler notificationHandler)
        {
            this.expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
            this.notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        }

        public async Task<EditExpenseCommandResult> Handle(EditExpenseCommand command)
        {
            var expense = Domain.Expenses.Expense.EditMainParameters(
                command.Id,
                command.Name,
                command.Description,
                command.Category,
                command.Amount,
                command.PaymentDate,
                command.IsActive
                );

            var success = await expenseRepository.Update(expense);

            return new EditExpenseCommandResult(success);
        }
    }
}
