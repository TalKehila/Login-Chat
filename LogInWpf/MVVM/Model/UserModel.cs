using System.Collections.ObjectModel;

namespace LogInWpf.MVVM.Model
{
	public class UserModel
	{
		public string UserName { get; set; } = string.Empty;
		public ObservableCollection<MessageModel> Messages { get; set; } = new ObservableCollection<MessageModel>();
		public string LastMessage
		{
			get
			{
				if (Messages.Any())
				{
					return Messages.Last().Message;
				}
				else
				{
					return string.Empty; // Or whatever default value you prefer
				}
			}
		}

	
	}
}