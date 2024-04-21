using LogInWpf.Core;
using LoginWpfLogic.Services;
using Microsoft.AspNetCore.Http;
using System.Windows;

namespace LogInWpf.MVVM.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public RelayCommand HomeViewCommand { get; set; }
		public RelayCommand ChatViewCommand { get; set; }
		public RelayCommand GameViewCommand { get; set; }
		public HomeViewModel HomeVm { get; set; }
		public ChatViewModel ChatVm { get; set; }
		public GameViewModel GameVm { get; set; }

		private object _currentView;

		public object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel()
		{
			HomeVm = new HomeViewModel();
			ChatVm = new ChatViewModel();
			GameVm = new GameViewModel();

			HomeViewCommand = new RelayCommand(
				execute: o => { CurrentView = HomeVm; },
				canExecute: o => true
			);
			ChatViewCommand = new RelayCommand(
				execute: o => { CurrentView = ChatVm; },
				canExecute: o => true
			);
			GameViewCommand = new RelayCommand(
				execute: o => { CurrentView = GameVm; },
				canExecute: o => true
			);

			CurrentView = HomeVm;
			InitializeAsync();
		}
		private async void InitializeAsync()
		{
			string token = TokenManager.Token;
			var httpClient = new HttpService(new System.Net.Http.HttpClient(), token);
			bool isTokenValid = await httpClient.IsTokenValid(token);

			if (isTokenValid)
			{
				HomeVm = new HomeViewModel();
				ChatVm = new ChatViewModel();
				GameVm = new GameViewModel();

				HomeViewCommand = new RelayCommand(
					execute: o => { CurrentView = HomeVm; },
					canExecute: o => true
				);
				ChatViewCommand = new RelayCommand(
					execute: o => { CurrentView = ChatVm; },
					canExecute: o => true
				);
				GameViewCommand = new RelayCommand(
					execute: o => { CurrentView = GameVm; },
					canExecute: o => true
				);

				CurrentView = HomeVm;

			}
			else
			{
				MessageBox.Show("Token is not valid. Please log in again");
			}
		}

	}
}
