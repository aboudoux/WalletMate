using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletMate.Infrastructure.Dto;
using IAuthorizationService = WalletMate.Infrastructure.Services.IAuthorizationService;

namespace WalletMate.WebApp.Controllers
{
    [Route("api/[controller]")]    
    [Authorize]
    public class AuthenticationController : Controller
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

        [HttpPost("[action]")]

        public async Task<IActionResult> Logout(Guid authKey)
        {
            await _authorizationService.Logoff(authKey);
            return Ok();
        }
    }
}