using BackGammon_API.Service.Interfaces;
using LoginWpfLogic.DTO;
using LoginWpfLogic.Services;
using MaterialDesignThemes.Wpf;
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
    public partial class AccountCreation : Window
    {
        private IUserManager _userManager = new UserService();
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        public AccountCreation()
        {
            
            InitializeComponent();
            RestartTheme();
        }

        private void themeToggle_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void RestartTheme()
        {
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void backToLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backtoLogin = new MainWindow();
            this.Close();
            backtoLogin.Show();
        }

        private async void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              var respone =  await _userManager.RegisterAsync(txtFirstName.Text, txtLastName.Text, txtEmail.Text,
                  txtUsername.Text, txtPassword.Password, txtConfirmPassword.Password);

                if (respone.IsSuccessStatusCode)
                {
                    //reseting the textboxes
                    txtFirstName.Text = string.Empty;
                    txtLastName.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtUsername.Text = string.Empty;
                    txtPassword.Password = string.Empty;
                    txtConfirmPassword.Password = string.Empty;
                    
                    //only test,in the future its will route to the homepage
                    MessageBox.Show("Registration confiremd:" + " " + txtUsername.Text);
                }
                else
                {
                    MessageBox.Show("Registration failed" + "\n" + "Please Fill the additional details");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
