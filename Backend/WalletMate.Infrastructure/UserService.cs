using System;
using Microsoft.AspNetCore.Http;
using WalletMate.Domain.Common;

namespace WalletMate.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }
        public string GetCurrentUserName()
        {
            return _httpContext.HttpContext.User.Identity.Name;
        }
    }
}