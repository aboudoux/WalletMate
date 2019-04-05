using System;
using System.Threading.Tasks;
using CoupleExpenses.Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace CoupleExpenses.WebApp.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly ICommandBus _commandBus;

        public ControllerBase(ICommandBus commandBus)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        }
       
        public async Task<IActionResult> SendCommandAsync(ICommand command)
        {
            try
            {
                await _commandBus.SendAsync(command);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}