using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoupleExpenses.WebApp.Controllers
{
    public class ControllerBase : Controller
    {
        public async Task<IActionResult> Handle(Func<Task> action)
        {
            try
            {
                await action();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}