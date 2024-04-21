using BackGammon_API.Service.Interfaces;
using LogInWpf.MVVM.ViewModel;
using LoginWpfLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogInWpf
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Window
    {
        

        public Lobby()
        {
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to exit?","Confirm exit",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                Application.Current.Properties.Remove("Token");
                TokenManager.Token = string.Empty;
                Close();
            }

        }

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{

		}
	}
}
