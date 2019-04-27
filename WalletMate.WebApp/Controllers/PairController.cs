using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletMate.Application.Pairs;
using WalletMate.Infrastructure.Dto;
using WalletMate.Infrastructure.Services;

namespace WalletMate.WebApp.Controllers
{
    [Route("api/[controller]")]    
    public class PairController
    {
        private readonly IUserProvider _userProvider;

        public PairController(IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }
        //[HttpGet("[action]")]
        //public async Task<IActionResult> All()
        //{
        //    var result = new List<PairUser>();
        //    result.Add(new );
        //}
    }

    public class PairUser
    {
        public PairUser(int pairId, string username)
        {
            PairId = pairId;
            Username = username;
        }
        public int PairId { get; }
        public string Username { get; }
    }
}