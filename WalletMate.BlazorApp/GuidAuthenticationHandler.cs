using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WalletMate.Application.Core;

namespace WalletMate.BlazorApp
{
    public class GuidAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
        private readonly IAuthorizationService _authorizationService;

        public GuidAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, 
            IAuthorizationService authorizationService) 
            : base(options, logger, encoder, clock)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var authKey = Guid.Parse(credentials[1]);

                var user = await _authorizationService.GetAssociatedUser(authKey);
                if(user == string.Empty)
                    return AuthenticateResult.Fail("Invalid Username or Password");

                var claims = new[] {
                    new Claim(ClaimTypes.Name, user),
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }
}