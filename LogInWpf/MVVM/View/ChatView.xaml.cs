using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LogInWpf.MVVM.View
{
	/// <summary>
	/// Interaction logic for ChatView.xaml
	/// </summary>
	public partial class ChatView : UserControl
	{
		//private HubConnection connection;


		public ChatView()
		{
			InitializeComponent();
			//connection = new HubConnectionBuilder()	
			//	.WithAutomaticReconnect()
				//.Build();

		}
	
	}

}