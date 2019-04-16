using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletMate.Application.Core;

namespace WalletMate.WebApp.Controllers
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