using System;
using System.Windows;
using System.Windows.Input;

namespace CharacterSheet
{
    /// <summary>
    /// Logica di interazione per LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private string UserName { set; get; }
        private string Password { set; get; }
        public SQLManager SQLMan { private set; get; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            UserName = UserNameBox.Text;
            Password = PasswordBox.Password;

            if (string.IsNullOrEmpty(UserName))
            {
                UserName = "loginApp";
                Password = "Prova12345";
            }

            try
            {
                SQLMan = new SQLManager(UserName, Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }
    }
}
