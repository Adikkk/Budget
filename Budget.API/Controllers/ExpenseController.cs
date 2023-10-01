using Budget.API.RequestsModels.Expense;
using Budget.API.ResponseModels.Expense;
using Budget.Application.Command.Abstractions;
using Budget.Application.Command.Commands.Expense.CreateExpenseCommand;
using Budget.Application.Command.Commands.Expense.DeleteExpenseCommand;
using Budget.Application.Command.Commands.Expense.EditExpenseCommand;
using Budget.Application.Query.Abstractions;
using Budget.Application.Query.Queries.Expenses.GetExpenseByIdQuery;
using Budget.Application.Query.Queries.Expenses.GetExpenseListQuery;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/expenses")]
    [Produces("application/json")]
    [ApiController]
    public class ExpenseController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ExpenseController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateExpenseRequestModel request)
        {
            try
            {
                var command = new CreateExpenseCommand(
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
                    var response = new CreateExpenseResponseModel()
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

                return BadRequest(request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {   
            var query = new GetExpenseListQuery();
            var result = await _queryDispatcher.ExecuteAsync(query);

            var respose = result.Expenses.Select(x => new GetExpenseListResponseModel()
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
            try
            {
                if(id == Guid.Empty)
                    return BadRequest();

                var query = new GetExpenseByIdQuery(id);

                var queryResult = await _queryDispatcher.ExecuteAsync(query);

                var response = new GetExpenseDetailsResponseModel()
                {
                    Id = queryResult.Expense.Id,
                    Name = queryResult.Expense.Name,
                    Description = queryResult.Expense.Description,
                    Category = queryResult.Expense.Category,
                    Amount = queryResult.Expense.Amount,
                    PaymentDate = queryResult.Expense.PaymentDate
                };

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update(EditExpenseRequestModel request)
        {
            var command = new EditExpenseCommand(
                request.Id,
                request.Name,
                request.Description,
                request.Category,
                request.Amount,
                request.PaymentDate,
                request.IsActive
                );

            var result = await _commandDispatcher.Dispatch(command);

            var response = new EditExpenseResponseModel()
            {
                Success = result.Success
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteExpenseCommand(id);

            var result = await _commandDispatcher.Dispatch(command);

            var response = new DeleteExpenseResponseModel()
            {
                Success = result.Success
            };

            return Ok(response);
        }
    }
}
