using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoupleExpenses.WebApp.Controllers;

namespace CoupleExpenses.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly Dictionary<Guid, string> _connectedUsers = new Dictionary<Guid, string>();

        private readonly List<User> _users = new List<User>
        {
            new User("aurelien", "0f46f2fb6f5a91c79e86acc5da7df95176b4e4c7"),
            new User("marie","5fa0bfcd909ca004073b086ed1843e6cac480f85") 
        };

        public Task<Guid> Authenticate(string username, string password)
        {
            if (_users.Any(x =>
                string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(x.Password, password, StringComparison.CurrentCultureIgnoreCase)))
            {
                var authKey = Guid.NewGuid();
                _connectedUsers.Add(authKey, username);
                return Task.FromResult(authKey);
            }

            return Task.FromResult(Guid.Empty);
        }

        public Task<string> GetAssociatedUser(Guid authKey)
        {
            if (_connectedUsers.ContainsKey(authKey))
                return Task.FromResult(_connectedUsers[authKey]);
            return Task.FromResult(string.Empty);
        }

        public Task Logoff(Guid authKey)
        {
            if (_connectedUsers.ContainsKey(authKey))
                _connectedUsers.Remove(authKey);
            return Task.CompletedTask;
        }
    }
}