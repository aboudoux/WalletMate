using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = CoupleExpenses.Infrastructure.Services.IAuthorizationService;

namespace CoupleExpenses.WebApp.Controllers
{
    [Route("api/[controller]")]    
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthenticationController(IAuthorizationService authorizationService) 
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User userInput) 
        {
            var authKey = await _authorizationService.Authenticate(userInput.Username, userInput.Password);

            if (authKey == Guid.Empty)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new AuthResult(userInput.Username, authKey));
        }        
    }
}