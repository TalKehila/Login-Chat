using System.Collections.Generic;

namespace RequestServer
{
	public  class Users
	{
		static public	List<KeyValuePair<string, string>> _users= new List<KeyValuePair<string, string>>();

		public static void AddUser(string Username, string Token)
		{
			if (CheckIfUserOnline(Username)==false)
			{
				_users.Add(new KeyValuePair<string, string>(Username, Token));
			}
		}
		public static void DelUser(string Username)
		{
			//_users.RemoveAll(u => u.Key == Username);
			foreach (KeyValuePair<string, string> item in _users)
			{
				if (item.Key == Username)
					_users.Remove(item);
			}
		}

		public static bool ValidateUser(string Username, string Token)
		{
			//return _users.Exists(u => u.Key == Username && u.Value == Token);
			foreach (KeyValuePair<string, string> item in _users)
			{
				if (item.Key == Username)
				{
					if (item.Value == Token)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}

			return false;
		}
		//_items.Add(new KeyValuePair<string, string>(foo, bar));


		public static bool CheckIfUserOnline(string Username)
		{

			//return _users.Exists(u => u.Key == Username);
			foreach (KeyValuePair<string, string> item in _users)
			{
				if (item.Key == Username)
					return true;
			}

			return false;
		}


	}
}
