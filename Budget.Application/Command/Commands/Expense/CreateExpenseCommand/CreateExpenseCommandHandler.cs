using Budget.Application.Command.Abstractions;
using Budget.Domain.Expenses;
using Budget.Domain.Shared;

namespace Budget.Application.Command.Commands.Expense.CreateExpenseCommand
{
    public class CreateExpenseCommandHandler : ICommandHandler<CreateExpenseCommand, CreateExpenseCommandResult>
    {
        private readonly IEventBus eventBus;
        private readonly IExpenseWriteOnlyRepository expenseRepository;
        private readonly ValidationNotificationHandler notificationHandler;

        public CreateExpenseCommandHandler(IEventBus eventBus, IExpenseWriteOnlyRepository expenseRepository, ValidationNotificationHandler notificationHandler)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
            this.notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        }

        public async Task<CreateExpenseCommandResult> Handle(CreateExpenseCommand command)
        {
            var newExpense = Domain.Expenses.Expense.CreateNew(
                command.Name,
                command.Description,
                command.Category,
                command.Amount,
                command.PaymentDate,
                command.IsActive
                );

            var success = await expenseRepository.Add(newExpense);

            if (success)
            {
                var expenseCreatedEvent = new ExpenseCreatedEvent(newExpense);

                eventBus.Publish(expenseCreatedEvent);
            }

            return new CreateExpenseCommandResult(newExpense.Id, newExpense.Name, newExpense.Description, newExpense.Category, newExpense.Amount, newExpense.PaymentDate, success);
        }
    }
}
