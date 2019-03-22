﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("getinfos")]
        public string GetInfos()
        {
            return "this is a secret information";
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthResult
    {
        public AuthResult(string username, Guid authKey)
        {
            Username = username;
            AuthKey = authKey;
        }

        public string Username { get; }
        public Guid AuthKey { get; }
    }
}