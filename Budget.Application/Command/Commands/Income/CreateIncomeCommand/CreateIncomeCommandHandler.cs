using Budget.Application.Command.Abstractions;
using Budget.Domain.Incomes;
using Budget.Domain.Shared;

namespace Budget.Application.Command.Commands.Income.CreateIncomeCommand
{
    public class CreateIncomeCommandHandler : ICommandHandler<CreateIncomeCommand, CreateIncomeCommandResult>
    {
        private readonly IEventBus eventBus;
        private readonly IIncomeWriteOnlyRepository incomeRepository;
        private readonly ValidationNotificationHandler notificationHandler;

        public CreateIncomeCommandHandler(IEventBus eventBus, IIncomeWriteOnlyRepository incomeRepository, ValidationNotificationHandler notificationHandler)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.incomeRepository = incomeRepository ?? throw new ArgumentNullException(nameof(incomeRepository));
            this.notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        }

        public async Task<CreateIncomeCommandResult> Handle(CreateIncomeCommand command)
        {
            var newIncome = Domain.Incomes.Income.CreateNew(
                command.Name,
                command.Description,
                command.Category,
                command.Amount,
                command.PaymentDate,
                command.IsActive
                );

            var success = await incomeRepository.Add(newIncome);

            if (success)
            {
                var incomeCreatedEvent = new IncomeCreatedEvent(newIncome);

                eventBus.Publish(incomeCreatedEvent);
            }

            return new CreateIncomeCommandResult(newIncome.Id, newIncome.Name, newIncome.Description, newIncome.Category, newIncome.Amount, newIncome.PaymentDate, success);
        }
    }
}
