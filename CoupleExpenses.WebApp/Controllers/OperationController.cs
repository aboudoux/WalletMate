using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using CoupleExpenses.Application.Periods;
using CoupleExpenses.Application.Periods.Queries;
using CoupleExpenses.Domain.Periods.ValueObjects;
using CoupleExpenses.Infrastructure.Dto;
using CoupleExpenses.WebApp.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleExpenses.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class OperationController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public OperationController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddSpending([FromBody]Spending input)
        {
            try
            {
                await _commandBus.SendAsync(new AddSpending(
                    PeriodId.From(input.PeriodId),
                    Amount.From(input.Amount),
                    Label.From(input.Label),
                    Pair.From(input.Pair),
                    SpendingOperationType.From(input.OperationType)));
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRecipe([FromBody]Recipe input)
        {
            await _commandBus.SendAsync(new AddRecipe(
                PeriodId.From(input.PeriodId),
                Amount.From(input.Amount),
                Label.From(input.Label),
                Pair.From(input.Pair),
                RecipeOperationType.From(input.OperationType)));
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IReadOnlyList<IPeriodOperation>> All(string periodId)
        {
            return await _queryBus.QueryAsync(new GetAllOperation(PeriodId.From(periodId)));
        }
    }
}