using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleExpenses.WebApp.Controllers
{
    [Route("api/[controller]")]    
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("[action]")]        
        public bool Login([FromBody]LoginDtoInput input)
        {
            Console.WriteLine(input.Password);
            return false;
        }      
    }

    public class LoginDtoInput
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}