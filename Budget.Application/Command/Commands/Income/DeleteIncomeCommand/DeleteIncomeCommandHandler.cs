using Budget.Application.Command.Abstractions;
using Budget.Domain.Incomes;

namespace Budget.Application.Command.Commands.Income.DeleteExpenseCommand
{
    public class DeleteIncomeCommandHandler : ICommandHandler<DeleteIncomeCommand, DeleteIncomeCommandResult>
    {
        private readonly IIncomeWriteOnlyRepository _incomeRepository;

        public DeleteIncomeCommandHandler(IIncomeWriteOnlyRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<DeleteIncomeCommandResult> Handle(DeleteIncomeCommand command)
        {
            var incomeToDelete = await _incomeRepository.GetByIdAsync(command.Id);

            var success = await _incomeRepository.Delete(incomeToDelete);

            return new DeleteIncomeCommandResult(success);
        }
    }
}
