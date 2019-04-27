using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletMate.Domain.Common;
using WalletMate.Infrastructure.Dto;

namespace WalletMate.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly Dictionary<Guid, string> _connectedUsers = new Dictionary<Guid, string>();
        private readonly IReadOnlyCollection<User> _users;

        public AuthorizationService(IConfigurationProvider configuration)
        {
            if(configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _users = configuration.GetUsers();
        }

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