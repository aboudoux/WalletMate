using System;
using CoupleExpenses.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace CoupleExpenses.Infrastructure
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