using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWpfLogic.Services
{
	public static class TokenManager
	{
		private static string token;

		public static string Token
		{
			get { return token; }
			set { token = value; }
		}

	}
}
