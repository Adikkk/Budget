using Budget.API.RequestsModels.Income;
using Budget.API.ResponseModels.Income;
using Budget.Application.Command.Abstractions;
using Budget.Application.Command.Commands.Income.CreateIncomeCommand;
using Budget.Application.Command.Commands.Income.DeleteExpenseCommand;
using Budget.Application.Command.Commands.Income.EditExpenseCommand;
using Budget.Application.Query.Abstractions;
using Budget.Application.Query.Queries.Incomes.GetIncomeByIdQuery;
using Budget.Application.Query.Queries.Incomes.GetIncomeListQuery;
using Budget.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/incomes")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ValidationNotificationHandler validationNotificationHandler;
        private readonly IQueryDispatcher _queryDispatcher;

        public IncomeController(
            ICommandDispatcher commandDispatcher, 
            ValidationNotificationHandler validationNotificationHandler,
            IQueryDispatcher queryDispatcher
            )
        {
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
            this.validationNotificationHandler = validationNotificationHandler ?? throw new ArgumentNullException(nameof(validationNotificationHandler));
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateIncomeRequestModel request)
        {
            var command = new CreateIncomeCommand(
                request.Name,
                request.Description,
                request.Category,
                request.Amount,
                request.PaymentDate,
                request.IsActive
                );
            var result = await _commandDispatcher.Dispatch(command);

            if (result.Success)
            {
                var response = new CreateIncomeResponseModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    Category = result.Category,
                    Amount = result.Amount,
                    PaymentDate = result.PaymentDate
                };

                return CreatedAtAction(null, response);
            }

            return BadRequest(validationNotificationHandler.Notifications);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetIncomeListQuery();
            var result = await _queryDispatcher.ExecuteAsync(query);

            var respose = result.Incomes.Select(x => new GetIncomeListResponseModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Category = x.Category,
                Amount = x.Amount,
                PaymentDate = x.PaymentDate
            });
            return Ok(respose);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetIncomeByIdQuery(id);

            var queryResult = await _queryDispatcher.ExecuteAsync(query);

            var response = new GetIncomeDetailsResponseModel()
            {
                Id = queryResult.Incomes.Id,
                Name = queryResult.Incomes.Name,
                Description = queryResult.Incomes.Description,
                Category = queryResult.Incomes.Category,
                Amount = queryResult.Incomes.Amount,
                PaymentDate = queryResult.Incomes.PaymentDate
            };

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditIncomeRequestModel request)
        {
            var command = new EditIncomeCommand(
                request.Id,
                request.Name,
                request.Description,
                request.Category,
                request.Amount,
                request.PaymentDate,
                request.IsActive
                );

            var result = await _commandDispatcher.Dispatch(command);

            var response = new EditIncomeResponseModel()
            {
                Success = result.Success
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteIncomeCommand(id);

            var result = await _commandDispatcher.Dispatch(command);

            var response = new DeleteIncomeResponseModel()
            {
                Success = result.Success
            };

            return Ok(response);
        }
    }
}
