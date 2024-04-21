using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using LoginWpfLogic.Services;
using MaterialDesignThemes.Wpf;
using System.Net.Http.Headers;


namespace LogInWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserManager _userManager = new UserService();
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        public MainWindow()
        {
            InitializeComponent();
            RestartTheme();
            Application.Current.Exit += OnApplicationExit;
        }
                
        
        
        private void themeToggle_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if(IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
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

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void RestartTheme()
        { ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            AccountCreation accountCreationWindow = new();
            this.Close();
            accountCreationWindow.ShowDialog();
        }

        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string token = await _userManager.LoginAsync(txtUsername.Text, txtPassword.Password);
                if (!string.IsNullOrEmpty(token))
                {
                    TokenManager.Token = token;
                    Application.Current.Properties["Token"] = token;

                    _userManager.AddTokenRequestHeaders();
                    string userName = _userManager.GetUserNameFromToken(token);
                    if (!string.IsNullOrEmpty(userName))
                    {
                        //MessageBox.Show(userName);
                        Lobby newLobby = new();
                        this.Close();
                        newLobby.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Username is null or empty.");
                    }
                }
                else
                {
                    MessageBox.Show("Token is null or empty.");
                }
            }
            catch (Exception ex)
            {
                // Display or log the exception message
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"An error occurred: {ex.Message}");
                txtWarning.Visibility = Visibility.Visible;
            }
        }
        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            Application.Current.Properties.Remove("Token");
        }

        
    }
}