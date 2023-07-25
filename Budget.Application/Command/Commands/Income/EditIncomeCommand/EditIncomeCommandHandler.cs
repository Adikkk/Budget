using Budget.Application.Command.Abstractions;
using Budget.Application.Command.Commands.Income.EditExpenseCommand;
using Budget.Domain.Incomes;
using Budget.Domain.Shared;

namespace Budget.Application.Command.Commands.Expense.EditExpenseCommand
{
    public class EditIncomeCommandHandler : ICommandHandler<EditIncomeCommand, EditIncomeCommandResult>
    {
        private readonly IIncomeWriteOnlyRepository _incomeRepository;

        public EditIncomeCommandHandler(IIncomeWriteOnlyRepository incomeRepository, ValidationNotificationHandler notificationHandler)
        {
            _incomeRepository = incomeRepository ?? throw new ArgumentNullException(nameof(incomeRepository));
        }

        public async Task<EditIncomeCommandResult> Handle(EditIncomeCommand command)
        {
            var income = Domain.Incomes.Income.EditMainParameters(
                command.Id,
                command.Name,
                command.Description,
                command.Category,
                command.Amount,
                command.PaymentDate,
                command.IsActive
                );

            var success = await _incomeRepository.Update(income);

            return new EditIncomeCommandResult(success);
        }
    }
}
