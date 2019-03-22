// This code is under Copyright (C) 2018 of Cegid SAS all right reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoupleExpenses.WebApp.Controllers
{
    public class AuthorizationService : IAuthorizationService
    {
        private Dictionary<Guid, string> _connectedUsers = new Dictionary<Guid, string>();

        private List<User> _users = new List<User>
        {
            new User { Username = "aurelien", Password = "0f46f2fb6f5a91c79e86acc5da7df95176b4e4c7" },
            new User { Username = "marie", Password = "5fa0bfcd909ca004073b086ed1843e6cac480f85" }
        };

        public Task<Guid> Authenticate(string username, string password)
        {
            if (_users.Any(x =>
                x.Username.ToLower() == username.ToLower() &&
                x.Password.ToLower() == password.ToLower()))
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