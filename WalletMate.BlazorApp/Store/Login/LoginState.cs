using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletMate.BlazorApp.Store.Login {
	public class LoginState 
	{
		public bool BadPassword { get; }
		public LoginState(bool badPassword)
		{
			BadPassword = badPassword;
		}
	}
}
